using System.Security.Claims;
using common.Api;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payment.Api.Data;
using Payment.Api.Models;
using Payment.Api.Models.Entity;
using Stripe;
using Stripe.Checkout;

namespace Payment.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StripeController : ControllerBase
{
    private readonly StripeModel model;
    private readonly TokenService tokenService;
    private readonly ProductService productService;
    private readonly CustomerService customerService;
    private readonly ChargeService chargeService;
    private static uint _nextId = 500;
    private static readonly object _lock = new object();

    private readonly IRepository<PaymentModel> repository;




    private const string endpointSecret = "whsec_d7568f90040ea12a9fd72a110a43055fc70d452b5e939ea678e6e6c75d10ca90";

    public StripeController(IRepository<PaymentModel> repository, AppDbContext context, IOptions<StripeModel> model, TokenService tokenService, ProductService productService, CustomerService customerService, ChargeService chargeService)
    {
        this.model = model.Value;
        this.tokenService = tokenService;
        this.productService = productService;
        this.customerService = customerService;
        this.chargeService = chargeService;
        this.repository = repository;


    }
    [HttpPost("pay")]
    public IActionResult Pay([FromBody] PaymentRequest paymentRequest, [FromHeader] int Id)
    {
        StripeConfiguration.ApiKey = model.SecretKey;

        // Fetch the price from Stripe to check if it's one-time or recurring
        var priceService = new PriceService();
        var price = priceService.Get(paymentRequest.PriceId);

        var sessionOptions = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {

                        Price = paymentRequest.PriceId,
                        Quantity = paymentRequest.Quantity,


                    }
                },
            Mode = price.Type == "recurring" ? "subscription" : "payment",
            SuccessUrl = "https://localhost:5001/Stripe/Success",
            CancelUrl = "https://localhost:5001/",
            Customer = paymentRequest.CustomerId,
            ClientReferenceId = Id.ToString()

        };

        var service = new SessionService();
        Session session = service.Create(sessionOptions);
        return Ok(session.Url);
    }
    [HttpPost("createProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] StripeProduct productRequest)
    {
        StripeConfiguration.ApiKey = model.SecretKey;

        if (productRequest == null || string.IsNullOrEmpty(productRequest.Name))
        {
            return BadRequest("Invalid product request.");
        }

        // Create product options
        var productOptions = new ProductCreateOptions
        {
            Id = productRequest.Id,
            Name = productRequest.Name,
            Description = productRequest.Description,
            Type = productRequest.Type,
            Active = productRequest.Active,
            Metadata = new Dictionary<string, string>
        {
            { "Category", productRequest.Category! },


        }
        };
        Console.WriteLine($"Product Name: {productRequest.Name}");
        Console.WriteLine($"Product Description: {productRequest.Description}");
        var product = await productService.CreateAsync(productOptions);

        // Create price options
        var priceOptions = new PriceCreateOptions
        {
            UnitAmount = productRequest.Price, // The price should be in cents already
            Currency = "usd",
            Recurring = productRequest.IsRecurring ? new PriceRecurringOptions
            {
                Interval = "month"
            } : null,
            Product = product.Id
        };

        var price = await new PriceService().CreateAsync(priceOptions);

        return Ok(new { ProductId = product.Id, PriceId = price.Id });
    }

    [HttpPost("createCustomer")]
    public async Task<IActionResult> CreateCustomer([FromBody] StripeCustomer stripeCustomer)
    {
        StripeConfiguration.ApiKey = model.SecretKey;



        var option = new CustomerCreateOptions
        {
            Email = stripeCustomer.Email,
            Name = stripeCustomer.Name,
            Metadata = new Dictionary<string, string>
            {
                { "PaymentId", stripeCustomer.Id.ToString() }
            }


        };
        var customer = await customerService.CreateAsync(option);

        return Ok(new { StripeCustomerId = customer.Id });
    }

    [HttpGet("getProducts")]
    public IActionResult GetAll()
    {
        StripeConfiguration.ApiKey = model.SecretKey;
        var options = new ProductListOptions { Expand = new List<string>() { "data.default_price" } };
        var products = productService.List(options);
        return Ok(products);
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> Webhook()
    {
        // var payload = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        // Console.WriteLine("payload: " + payload);
        // return Ok();
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret);
            uint id;
            lock (_lock)
            {
                id = _nextId++;
            }
            // Handle the event
            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                if (session!.Status == "complete" && session.CustomerDetails.Email != null)
                {

                    var custermerEmail = session.CustomerDetails.Email;
                    var str = session.ClientReferenceId;
                    uint result = uint.Parse(str);
                    var payment = new PaymentModel
                    {
                        Id = id,
                        CustomerEmail = custermerEmail,
                        PaymentStatus = 1,
                        DueAmount = (long)session.AmountSubtotal!,
                        TotalPaidAmount = (decimal)session.AmountTotal!,
                        TotalAmount = (decimal)session.AmountTotal!,
                        PaymentDetails = new List<PaymentDetail>
                        {
                            new PaymentDetail
                            {
                                StripePaymentId = session.Id,
                                AmountPaid = (decimal)session.AmountTotal!,
                                Currency = session.Currency,
                                PaymentDate = DateTime.UtcNow,
                                PaymentMethod = session.PaymentMethodTypes?.FirstOrDefault(),
                                TransactionId = session.PaymentIntentId
                            }
                        },
                        Address = new Models.Entity.Address
                        {
                            AddressLine = session.Invoice.CustomerAddress.Line1,
                            City = session.Invoice.CustomerAddress.City,
                            ZipCode = session.Invoice.CustomerAddress.PostalCode,
                            Country = session.Invoice.CustomerAddress.Country
                        },
                        Registrations = new List<RegistrationRef>()
                        {
                            new RegistrationRef
                            {
                                RegistrationId = result
                            }
                        }


                    };

                    await repository.CreateAsync(payment);
                    Console.WriteLine("Payment Successful " + session.CustomerDetails.Email);

                }
                else
                {
                    Console.WriteLine("Payment Failed");
                }
            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e.Data);
        }
    }

}








