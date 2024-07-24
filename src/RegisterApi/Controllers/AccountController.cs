using System.Text;
using CommonLib;
using CommonLib.Models;
using Microsoft.AspNetCore.Mvc;
using static RegisterApi.Dtos;

namespace RegisterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IRepositorySql<User> repository;


    public AccountController(IRepositorySql<User> repository)
    {
        this.repository = repository;
    }
    [HttpPost]
    public async Task<ActionResult<User>> PostAsync([FromForm] UserRegisterDto userDto)
    {
        byte[]? file = null;

        if (userDto.document != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await userDto.document.CopyToAsync(memoryStream);
                file = memoryStream.ToArray();
            }
        }
        var user = new User
        {


            Title = userDto.title,
            FirstName = userDto.first_name,
            SecondName = userDto.second_name,
            Surname = userDto.surname,
            Gender = userDto.gender,
            AgeRange = userDto.age_range,
            Email = userDto.email,
            JobTitle = userDto.job_title,
            CountryId = userDto.country_id,
            City = userDto.city,
            AddressLine1 = userDto.address,
            PoCode = userDto.postal_code,
            OrganisationId = userDto.organisation_id,
            Telephone = userDto.telephone,
            Mobile = userDto.phone,
            Secter = userDto.sectorType,
            Document = file

        };

        await repository.CreateAsync(user);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(uint id)
    {
        var user = await repository.GetAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }
}
