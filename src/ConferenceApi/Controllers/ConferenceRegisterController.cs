using common.Api;
using ConferenceApi.Entity;
using Microsoft.AspNetCore.Mvc;
using static ConferenceApi.RegisterDtos;

namespace ConferenceApi.Controllers;

[ApiController]
[Route("conference-register")]
public class ConferenceRegisterController : ControllerBase
{
    private readonly IRepository<Register> _repository;

    private static uint _nextId = 100; // Start from 100
    private static readonly object _lock = new object(); // Lock for thread safety
    private static readonly Random Random = new Random();


    public ConferenceRegisterController(IRepository<Register> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(uint id)
    {
        var register = await _repository.GetAsync(id);
        if (register is null)
        {
            return NotFound();
        }
        return Ok(register);
    }
    [HttpPost]
    public async Task<ActionResult<RegisterDtos>> PostAsync(RegisterCreateDto dto)
    {
        try
        {
            if (dto == null)
            {
                return BadRequest("RegisterCreateDto cannot be null.");
            }

            uint id;
            lock (_lock)
            {
                id = _nextId++;
            }

            decimal addonsTotalAmount = 0;
            if (dto.SelectedAddons != null && dto.SelectedAddons.Any())
            {
                addonsTotalAmount = dto.SelectedAddons
                .Sum(a => a.Amount * a.Quantity);
            }

            // Calculate the final total amount (conference amount + addons - discount)
            decimal totalAmount = dto.conferenceAmount + addonsTotalAmount - dto.DiscountAmount;

            var register = new Register
            {

                Id = id,
                Conference_Id = dto.ConferenceId,
                User_Id = dto.UserId,
                Type = dto.Type,
                ConferenceAmount = dto.conferenceAmount,
                TotalAmount = totalAmount,
                DiscountAmount = dto.DiscountAmount,
                RegistrationDate = DateTimeOffset.Now,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SelectedAddons = dto.SelectedAddons!.Select(a => new SelectedAddon
                {
                    AddonId = a.AddonId,
                    Title = a.Title,
                    Amount = a.Amount,
                    Quantity = a.Quantity


                }).ToList(),
                TermsStatus = dto.TermsStatus!.Select(t => new TermsStatus
                {
                    TermId = t.TermId,
                    Accepted = t.Accepted
                }).ToList(),

            };

            await _repository.CreateAsync(register);
            var response = new
            {
                message = "Registration successful.",
                totalAmount

            };

            return Ok(response);
        }
        catch (Exception)
        {
            throw;
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(uint id, RegisterUpdateDto dto)
    {
        var register = await _repository.GetAsync(id);
        if (register is null)
        {
            return NotFound();
        }

        register.User_Id = dto.UserId;
        register.Conference_Id = dto.ConferenceId;
        register.Type = dto.Type;
        register.RegistrationDate = dto.RegistrationDate;

        register.SelectedAddons = dto.SelectedAddons!.Select(a => new SelectedAddon
        {
            AddonId = a.AddonId,
            Title = a.Title,
            Amount = a.Amount,
            Quantity = a.Quantity
        }).ToList();

        register.TermsStatus = dto.TermsStatus!.Select(t => new TermsStatus
        {
            TermId = t.TermId,
            Accepted = t.Accepted
        }).ToList();



        decimal TotalCalAmount = dto.SelectedAddons!.Sum(a => a.Amount * a.Quantity);
        decimal DiscountAmount = dto.AppliedCouponCode != null ?
        (dto.AppliedCouponCode.DiscountValue * TotalCalAmount / 100) : dto.AppliedCouponCode!.DiscountValue;

        decimal NetAmount = TotalCalAmount - DiscountAmount;

        register.TotalAmount = TotalCalAmount;
        register.DiscountAmount = DiscountAmount;

        register.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(register);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(uint id)
    {
        var register = await _repository.GetAsync(id);
        if (register is null)
        {
            return NotFound();
        }
        await _repository.RemoveAsync(id);
        return NoContent();
    }
}
