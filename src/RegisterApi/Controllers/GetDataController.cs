using CommonLib.Models;
using CommonLib.MySql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RegisterApi.Controllers;

[ApiController]
[Route("get-data")]
public class GetDataController : ControllerBase
{
    private readonly MySqlDbContext mySqlDbContext;

    public GetDataController(MySqlDbContext mySqlDbContext)
    {
        this.mySqlDbContext = mySqlDbContext;
    }

    [HttpGet("countries")]
    public async Task<IEnumerable<ProfileCountry>> Get()
    {
        return await mySqlDbContext.ProfileCountries.ToListAsync();
    }
    [HttpGet("age-range")]
    public async Task<IEnumerable<ProfileAgeRange>> GetProfile()
    {
        return await mySqlDbContext.ProfileAgeRanges.ToListAsync();
    }
    [HttpGet("titles")]
    public async Task<IEnumerable<ProfileTitle>> GetTitles()
    {
        return await mySqlDbContext.ProfileTitles.ToListAsync();
    }
    [HttpGet("gender")]
    public async Task<IEnumerable<ProfileGender>> GetGender()
    {
        return await mySqlDbContext.ProfileGenders.ToListAsync();
    }
   


}
