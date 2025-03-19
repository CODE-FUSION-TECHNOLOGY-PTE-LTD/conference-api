using common.Api;
using CommonLib;
using ConferenceApi.Entity;
using Microsoft.AspNetCore.Mvc;
using static ConferenceApi.Dtos;

namespace ConferenceApi.Controllers;

[ApiController]
[Route("config")]

public class ConfigController : ControllerBase
{
    private readonly IRepository<Config> _repository;
    private static uint _nextId = 1000;
    private static readonly object _lock = new object();
    private static readonly Random Random = new Random();


    public ConfigController(IRepository<Config> repository)
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
        var config = await _repository.GetAsync(id);
        if (config is null)
        {
            return NotFound();
        }
        return Ok(config);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(ConfigCreateDto config)
    {

        uint id;
        lock (_lock)
        {
            id = _nextId++;
        }
        var conf = new Config
        {
            Id = id,
            OrganizationId = config.OrganizationId,
            Sectors = config.Sectors?.Select(s => new Sectors
            {
                Id = Guid.NewGuid(),
                Title = s
            }).ToList()
        };
        await _repository.CreateAsync(conf);
        return Ok(conf);

    }
    [HttpGet("by-organization/{organizationId}")]
    public async Task<IActionResult> GetByOrganizationIdAsync(int organizationId)
    {
        var configs = await _repository.FindAsync(c => c.OrganizationId == organizationId);
        if (!configs.Any())
        {
            return NotFound($"No configurations found for OrganizationId: {organizationId}");
        }
        return Ok(configs);
    }

}
