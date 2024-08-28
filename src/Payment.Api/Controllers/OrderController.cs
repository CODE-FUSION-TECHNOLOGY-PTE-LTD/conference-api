using common.Api;
using Microsoft.AspNetCore.Mvc;
using Payment.Api.Models.Entity;
using static Payment.Api.Dtos;

namespace Payment.Api.Controllers;

[ApiController]
[Route("order")]
public class OrderController : ControllerBase
{
    private readonly IRepository<OrderModel> repository;

    private static uint _nextId = 1000;
    private static readonly object _lock = new object();
    private static readonly Random Random = new Random();

    public OrderController(IRepository<OrderModel> repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await repository.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(uint id)
    {
        var order = await repository.GetAsync(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderCreateDto orderCreateDto)
    {
        uint id;
        lock (_lock)
        {
            id = _nextId++;
        }
        var order = new OrderModel
        {


            Id = id,
            UserId = orderCreateDto.UserId,
            Registrations = orderCreateDto.Registrations.Select(r => new RegistrationRef { RegistrationId = r.RegistrationId }).ToList(),
            Address = orderCreateDto.Address == null ? null : new Address
            {
                AddressLine = orderCreateDto.Address.AddressLine,
                City = orderCreateDto.Address.City,
                ZipCode = orderCreateDto.Address.ZipCode,
                Country = orderCreateDto.Address.Country
            },
            TotalAmount = orderCreateDto.TotalAmount,
            DueAmount = orderCreateDto.DueAmount,
            TotalPaidAmount = orderCreateDto.TotalPaidAmount,
            PaymentStatus = orderCreateDto.PaymentStatus,
            PaymentDetails = orderCreateDto.PaymentDetails.Select(p => new PaymentDetail
            {
                StripePaymentId = p.StripePaymentId,
                AmountPaid = p.AmountPaid,
                Currency = p.Currency,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod,
                TransactionId = p.TransactionId
            }).ToList(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,


        };

        await repository.CreateAsync(order);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(uint id, [FromBody] OrderUpdateDto orderUpdateDto)
    {
        var order = await repository.GetAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        order.UserId = orderUpdateDto.UserId;
        order.Registrations = orderUpdateDto.Registrations.Select(r => new RegistrationRef { RegistrationId = r.RegistrationId }).ToList();
        order.Address = orderUpdateDto.Address == null ? null : new Address
        {
            AddressLine = orderUpdateDto.Address.AddressLine,
            City = orderUpdateDto.Address.City,
            ZipCode = orderUpdateDto.Address.ZipCode,
            Country = orderUpdateDto.Address.Country
        };
        order.TotalAmount = orderUpdateDto.TotalAmount;
        order.DueAmount = orderUpdateDto.DueAmount;
        order.TotalPaidAmount = orderUpdateDto.TotalPaidAmount;
        order.PaymentStatus = orderUpdateDto.PaymentStatus;
        order.PaymentDetails = orderUpdateDto.PaymentDetails.Select(p => new PaymentDetail
        {
            StripePaymentId = p.StripePaymentId,
            AmountPaid = p.AmountPaid,
            Currency = p.Currency,
            PaymentDate = p.PaymentDate,
            PaymentMethod = p.PaymentMethod,
            TransactionId = p.TransactionId
        }).ToList();
        order.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(order);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var order = await repository.GetAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        await repository.RemoveAsync(id);
        return Ok();
    }
}
