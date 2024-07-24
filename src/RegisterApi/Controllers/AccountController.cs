using System.Text;
using common.Api;
using CommonLib;
using CommonLib.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RegisterApi.fileUpload.services;

using static RegisterApi.Dtos;

namespace RegisterApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IRepositorySql<User> repository;
    private readonly IRepository<User> mongoRepository;

    private readonly ManageFile manageFile;



    public AccountController(IRepositorySql<User> repository, ManageFile manageFile, IRepository<User> mongoRepository)
    {
        this.repository = repository;
        this.manageFile = manageFile;
        this.mongoRepository = mongoRepository;

    }
    [HttpPost("register")]
    public async Task<ActionResult<User>> PostAsync([FromForm] UserRegisterDto userDto)
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
        await mongoRepository.CreateAsync(user);
        
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
