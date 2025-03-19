using CommonLib.Models;
using CommonLib.MySql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RegisterApi.Controllers;

[ApiController]
[Route("get-data")]
public class GetDataController : ControllerBase
{
    private readonly MySqlDbContext _mySqlDbContext;


    public GetDataController(MySqlDbContext mySqlDbContext)
    {
        _mySqlDbContext = mySqlDbContext;

    }
    [HttpGet("countries")]
    public async Task<IEnumerable<ProfileCountry>> Get()
    {
        return await _mySqlDbContext.ProfileCountries.ToListAsync();
    }
    [HttpGet("age-range")]
    public async Task<IEnumerable<ProfileAgeRange>> GetProfile()
    {
        return await _mySqlDbContext.ProfileAgeRanges.ToListAsync();
    }
    [HttpGet("titles")]
    public async Task<IEnumerable<ProfileTitle>> GetTitles()
    {
        return await _mySqlDbContext.ProfileTitles.ToListAsync();
    }
    [HttpGet("gender")]
    public async Task<IEnumerable<ProfileGender>> GetGender()
    {
        return await _mySqlDbContext.ProfileGenders.ToListAsync();
    }
    [HttpGet("departments")]
    public async Task<IEnumerable<Department>> GetDepartments()
    {
        return await _mySqlDbContext.Departments.ToListAsync();
    }
    [HttpGet("organizations")]
    public async Task<IEnumerable<Organisation>> GetOrganisations()
    {
        return await _mySqlDbContext.Organisations.ToListAsync();
    }



}
