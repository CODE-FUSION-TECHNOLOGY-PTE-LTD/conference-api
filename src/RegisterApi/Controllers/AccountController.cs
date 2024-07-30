using System.Text;
using common.Api;
using CommonLib;
using CommonLib.Models;
using CommonLib.MySql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegisterApi.fileUpload.services;

using testCommon;

using static RegisterApi.Dtos;

namespace RegisterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IRepositorySql<User> repository;
    private readonly IRepository<User> mongoRepository;

    private readonly MySqlDbContext mySqlDbContext;

    private readonly ManageFile manageFile;

   







    public AccountController( IRepositorySql<User> repository, ManageFile manageFile, IRepository<User> mongoRepository, MySqlDbContext mySqlDbContext)
    {
        this.repository = repository;
        this.manageFile = manageFile;
        this.mongoRepository = mongoRepository;
        this.mySqlDbContext = mySqlDbContext;
    





    }
    [HttpPost("register")]
    public async Task<ActionResult<User>> PostAsync([FromForm] UserRegisterDto userDto)
    {

        string? file = null;

        if (userDto.document != null)
        {
            file = await manageFile.UploadFile(userDto.document); // Call the instance method
        }

        if (!uint.TryParse(userDto.country_id, out uint countryId))
        {
            return BadRequest("Invalid country ID format");
        }

        // Fetch country data to get the World Bank income group using LINQ
        var country = await mySqlDbContext.ProfileCountries
            .Where(c => c.Id == countryId)
            .Select(c => new { c.WorldBankIncomeGroup })
            .FirstOrDefaultAsync();

        if (country == null)
        {
            return BadRequest("Invalid country ID");
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
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
        {
            User = user,
            country.WorldBankIncomeGroup,
         
        });


    }
    // [HttpPost("publish")]
    // public async Task<IActionResult> Publish()
    // {
    //     var message = new BalalnceUpdate
    //     {
    //         Type = "test",
    //         Amount = 100,
    //     };

    //     var requestHandle = client.Create(message);
    //     var response = await requestHandle.GetResponse<NewBalalnce>();
    //     return Ok(response.Message);
    // }

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
    [HttpGet("check-user")]
    public async Task<ActionResult<List<User>>> CheckUser(string email)
    {
        try
        {
            var users = await repository.GetByEmailAsync(email);

            return Ok(users);
        }
        catch (NullReferenceException)
        {

            return NotFound();
        }
    }
    [HttpPut("register/{id}")]
    public async Task<ActionResult<User>> PutAsync(uint id, [FromForm] UserUpdateDto userDto)
    {
        string? file = null;

        if (userDto.document != null)
        {
            file = await manageFile.UploadFile(userDto.document); // Call the instance method
        }




        var user = new User
        {

            Title = userDto.title,
            FirstName = userDto.first_name,
            SecondName = userDto.second_name,
            Surname = userDto.surname,
            Gender = userDto.gender,
            AgeRange = userDto.age_range,
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
        await repository.UpdateAsync(user);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(uint id)
    {
        var user = await repository.GetAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        await repository.RemoveAsync(id);
        return NoContent();
    }

}
