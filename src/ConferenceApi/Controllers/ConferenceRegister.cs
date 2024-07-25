using common.Api;
using ConferenceApi.Entity;
using Microsoft.AspNetCore.Mvc;
using static ConferenceApi.RegisterDtos;

namespace ConferenceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ConferenceRegister : ControllerBase
{
    private readonly IRepository<Register> _repository;

    private static uint _nextId = 100; // Start from 100
    private static readonly object _lock = new object(); // Lock for thread safety
    private static readonly Random Random = new Random();


    public ConferenceRegister(IRepository<Register> repository)
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

            decimal totalCalAmount = dto.SelectedAddons!.Sum(a => a.Amount * a.Quantity);
            decimal DiscountAmount = dto.AppliedCouponCode != null ?
            (dto.AppliedCouponCode.DiscountValue * totalCalAmount / 100) : dto.AppliedCouponCode!.DiscountValue;

            // Generate a random uint in a safe range
            uint id;
            lock (_lock)
            {
                id = _nextId++;
            }
            // Map the AppliedCouponDto to AppliedCoupon
            var appliedCoupon = dto.AppliedCouponCode != null ? new AppliedCoupon
            {
                Code = dto.AppliedCouponCode.Code,
                DiscountType = dto.AppliedCouponCode.DiscountType,
                DiscountValue = dto.AppliedCouponCode.DiscountValue
            } : null;

            decimal NetAmount = totalCalAmount - DiscountAmount;

            var register = new Register
            {

                Id = id,
                Conference_Id = dto.ConferenceId,
                User_Id = dto.UserId,
                Type = dto.Type,
                TotalAmount = totalCalAmount,
                NetAmount = NetAmount,
                DiscountAmount = DiscountAmount,
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
                AppliedCouponCode = appliedCoupon
            };

            await _repository.CreateAsync(register);

            return Ok(register);
        }
        catch (Exception)
        {
            throw;
        }

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
