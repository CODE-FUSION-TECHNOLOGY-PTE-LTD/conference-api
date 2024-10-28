using CommonLib.Models;
using CommonLib.MySql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RegisterApi.Controllers;

[ApiController]
[Route("get-data")]
public class GetDataController : ControllerBase
{
    private readonly GlobalParametersDbContext globalParameterDbContext;
    private readonly IdentityDbContext identityDbContext;

    public GetDataController(GlobalParametersDbContext globalParameterDbContext, IdentityDbContext identityDbContext)
    {
        this.globalParameterDbContext = globalParameterDbContext;
        this.identityDbContext = identityDbContext;
    }

    [HttpGet("countries")]
    public async Task<IEnumerable<ProfileCountry>> GetCountries()
    {
        return await globalParameterDbContext.ProfileCountris.ToListAsync();
    }
    [HttpGet("age-range")]
    public async Task<IEnumerable<ProfileAgeRange>> GetProfile()
    {
        return await globalParameterDbContext.ProfileAgeRanges.ToListAsync();
    }
    [HttpGet("titles")]
    public async Task<IEnumerable<ProfileTitle>> GetTitles()
    {
        return await globalParameterDbContext.ProfileTitles.ToListAsync();
    }
    [HttpGet("gender")]
    public async Task<IEnumerable<ProfileGender>> GetGender()
    {
        return await globalParameterDbContext.ProfileGenders.ToListAsync();
    }
    [HttpGet("departments")]
    public async Task<IEnumerable<Department>> GetDepartments()
    {
        return await identityDbContext.Departments.ToListAsync();
    }
    [HttpGet("organizations")]
    public async Task<IEnumerable<Organisation>> GetOrganisations()
    {
        return await identityDbContext.Organisations.ToListAsync();
    }



}
