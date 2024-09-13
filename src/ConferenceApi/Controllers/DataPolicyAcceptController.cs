using common.Api;
using ConferenceApi.Entity;
using Microsoft.AspNetCore.Mvc;
using static ConferenceApi.Dtos;

namespace ConferenceApi.Controllers;

[ApiController]
[Route("data-policy-accept")]

public class DataPolicyAcceptController : ControllerBase
{
    private readonly IRepository<AcceptPolicy> _repository;
    private static uint _nextId = 2000;
    private static readonly object _lock = new object();
    private static readonly Random Random = new Random();


    public DataPolicyAcceptController(IRepository<AcceptPolicy> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreatePolicyAcceptDto dto)
    {

        uint id;
        lock (_lock)
        {
            id = _nextId++;
        }

        var policy = new AcceptPolicy
        {
            Id = id,
            User_Id = dto.UserId,
            ShareDataWithExhibitorsAccept = dto.ShareDataWithExhibitorsAccept,
            ShareDataWithSponsorsAccept = dto.ShareDataWithSponsorsAccept,
            AcceptDate = DateTimeOffset.Now
        };

        await _repository.CreateAsync(policy);
        return Ok(policy);
    }

}
