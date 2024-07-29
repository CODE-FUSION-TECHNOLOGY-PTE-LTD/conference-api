using common.Api;
using ConferenceApi.Entity;
using Microsoft.AspNetCore.Mvc;
using static ConferenceApi.Dtos;

namespace ConferenceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ConferenceController : ControllerBase
{
    private readonly IRepository<Conference> _repository;
    private static readonly Random Random = new Random();

    public ConferenceController(IRepository<Conference> repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        // var conference = (await _repository.GetAllAsync()).Select(c => c.AsDto());
        // return Ok(conference);
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(uint id)
    {
        var conference = await _repository.GetAsync(id);
        if (conference is null)
        {
            return NotFound();
        }
        return Ok(conference);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(ConferenceCreateDto dto)
    {
        if (dto == null)
        {
            return BadRequest("ConferenceCreateDto cannot be null.");
        }
        // Generate a random uint in a safe range
        uint randomUInt = (uint)Random.Next(0, int.MaxValue);
        var conference = new Conference
        {
            Id = randomUInt,
            Title = dto.Title,
            Description = dto.Description,
            OrganizationId = dto.OrganizationId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,

            Categories = dto.Categories!.Select(c => new Category
            {
                Id = Guid.NewGuid(),
                Title = c.Title,
                DateOptions = c.DateOptions
            }).ToList(),

            Addons = dto.Addons!.Select(a => new Addon
            {
                Id = Guid.NewGuid(),
                Title = a.Title,
                Description = a.Description
            }).ToList(),

            Coupons = dto.Coupons!.Select(c => new Coupon
            {
                Id = Guid.NewGuid(),
                Code = c.Code,
                DiscountType = c.DiscountType,
                DiscountValue = c.DiscountValue,
            }).ToList(),
            Terms = dto.Terms!.Select(t => new Term
            {
                Id = Guid.NewGuid(),
                Title = t.Title,
                Description = t.Description
            }).ToList(),
            Location = new Location
            {
                Id = Guid.NewGuid(),
                Venue = dto.Location?.Venue,
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    StreetAddress = dto.Location?.Address?.StreetAddress,
                    City = dto.Location?.Address?.City,
                    ZipCode = dto.Location?.Address?.ZipCode,
                    Country = dto.Location?.Address?.Country
                }
            },
            Contact = new Contact
            {
                Id = Guid.NewGuid(),
                Email = dto.Contact?.Email,
                Phone = dto.Contact?.Phone
            }


        };
        await _repository.CreateAsync(conference);

        return Ok(conference);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(uint id, ConferenceUpdateDto dto)
    {
        var conference = await _repository.GetAsync(id);
        if (conference is null)
        {
            return NotFound();
        }
        conference.Title = dto.Title;
        conference.Description = dto.Description;
        conference.OrganizationId = dto.OrganizationId;
        conference.StartDate = dto.StartDate;
        conference.EndDate = dto.EndDate;

        conference.Categories = dto.Categories!.Select(c => new Category
        {
            Title = c.Title,
            DateOptions = c.DateOptions
        }).ToList();

        conference.Addons = dto.Addons!.Select(a => new Addon
        {
            Title = a.Title,
            Description = a.Description
        }).ToList();

        conference.Coupons = dto.Coupons!.Select(c => new Coupon
        {
            Code = c.Code,
            DiscountType = c.DiscountType,
            DiscountValue = c.DiscountValue,
        }).ToList();

        conference.Terms = dto.Terms!.Select(t => new Term
        {
            Title = t.Title,
            Description = t.Description
        }).ToList();

        conference.Location = new Location
        {
            Venue = dto.Location?.Venue,
            Address = new Address
            {
                StreetAddress = dto.Location?.Address?.StreetAddress,
                City = dto.Location?.Address?.City,
                ZipCode = dto.Location?.Address?.ZipCode,
                Country = dto.Location?.Address?.Country
            }
        };

        conference.Contact = new Contact
        {
            Email = dto.Contact?.Email,
            Phone = dto.Contact?.Phone
        };
        await _repository.UpdateAsync(conference);
        return Ok(conference);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(uint id)
    {
        var conference = await _repository.GetAsync(id);
        if (conference is null)
        {
            return NotFound();
        }
        await _repository.RemoveAsync(id);
        return NoContent();
    }
}
