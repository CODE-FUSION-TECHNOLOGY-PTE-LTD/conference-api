using common.Api;
using ConferenceApi.Entity;
using ConferenceApi.OrganisationRegisterDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApi.Controllers
{
    [ApiController]
    [Route("group-register")]
    public class OrganisationRegisterController : ControllerBase
    {
        private readonly IRepository<Organisations> _organisationRepository;
        private static readonly Random _random = new Random(); 
        private readonly MySqlRepository<User> repository;

        public OrganisationRegisterController(IRepository<Organisations> organisationRepository)
        {
            _organisationRepository = organisationRepository;
        }

        // GET: /organisation/{id}
        [HttpGet("organisation/{id}")]
        public async Task<ActionResult<OrganisationDto>> GetOrganisation(uint id)
        {
            var organisation = await _organisationRepository.GetAsync(id);
            if (organisation == null)
            {
                return NotFound();
            }

            // Map the entity to DTO
            var organisationDto = new OrganisationDto
            {
                Id = organisation.Id,
                OrganisationName = organisation.OrganisationName,
                Title = organisation.Title,
                FirstName = organisation.FirstName,
                SurName = organisation.SurName,
                Email = organisation.Email,
                AlternativeEmail = organisation.AlternativeEmail,
                ZopCode = organisation.ZopCode,
                City = organisation.City,
                Location = organisation.Location,
                Phone = organisation.Phone,
                Mobile = organisation.Mobile,
                Members = organisation.Members.Select(m => new MemberDto
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    Name = m.Name,
                    Amount = m.Amount,
                    Orders = m.Orders.Select(o => new OrderDto
                    {
                        Id = o.Id,
                        Amount = o.Amount
                    }).ToList()
                }).ToList()
            };

            return Ok(organisationDto);
        }

        // POST: /organisation
        [HttpPost("organisation")]
        public async Task<ActionResult<OrganisationRegisterDtos.OrganisationDto>> CreateOrganisation(OrganisationDto organisationDto)
        {
            // Map DTO to entity
            uint randomUInt = (uint)_random.Next(0, int.MaxValue); // Use the instance to generate a random number
            var organisation = new Organisations
            {
                Id = randomUInt,
                OrganisationName = organisationDto.OrganisationName,
                Title = organisationDto.Title,
                FirstName = organisationDto.FirstName,
                SurName = organisationDto.SurName,
                Email = organisationDto.Email,
                AlternativeEmail = organisationDto.AlternativeEmail,
                ZopCode = organisationDto.ZopCode,
                City = organisationDto.City,
                Location = organisationDto.Location,
                Phone = organisationDto.Phone,
                Mobile = organisationDto.Mobile,
                Members = organisationDto.Members.Select(m => new Member
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    Name = m.Name,
                    Amount = m.Amount,
                    Orders = m.Orders.Select(o => new Order
                    {
                        Id = o.Id,
                        Amount = o.Amount
                    }).ToList()
                }).ToList()
            };

            await _organisationRepository.CreateAsync(organisation);

            // Map back to DTO for return
            organisationDto.Id = randomUInt;
            return CreatedAtAction(nameof(GetOrganisation), new { id = organisationDto.Id }, organisationDto);
        }

        // PUT: /organisation/{id}
        [HttpPut("organisation/{id}")]
        public async Task<ActionResult> UpdateOrganisation(uint id, OrganisationDto updatedOrganisationDto)
        {
            if (id != updatedOrganisationDto.Id)
            {
                return BadRequest();
            }

            var existingOrganisation = await _organisationRepository.GetAsync(id);
            if (existingOrganisation == null)
            {
                return NotFound();
            }

            // Map DTO to entity
            var updatedOrganisation = new Organisations
            {
                Id = updatedOrganisationDto.Id,
                OrganisationName = updatedOrganisationDto.OrganisationName,
                Title = updatedOrganisationDto.Title,
                FirstName = updatedOrganisationDto.FirstName,
                SurName = updatedOrganisationDto.SurName,
                Email = updatedOrganisationDto.Email,
                AlternativeEmail = updatedOrganisationDto.AlternativeEmail,
                ZopCode = updatedOrganisationDto.ZopCode,
                City = updatedOrganisationDto.City,
                Location = updatedOrganisationDto.Location,
                Phone = updatedOrganisationDto.Phone,
                Mobile = updatedOrganisationDto.Mobile,
                Members = updatedOrganisationDto.Members.Select(m => new Member
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    Name = m.Name,
                    Amount = m.Amount,
                    Orders = m.Orders.Select(o => new Order
                    {
                        Id = o.Id,
                        Amount = o.Amount
                    }).ToList()
                }).ToList()
            };

            await _organisationRepository.UpdateAsync(updatedOrganisation);
            return NoContent();
        }

        // DELETE: /organisation/{id}
        [HttpDelete("organisation/{id}")]
        public async Task<ActionResult> DeleteOrganisation(uint id)
        {
            var organisation = await _organisationRepository.GetAsync(id);
            if (organisation == null)
            {
                return NotFound();
            }

            await _organisationRepository.RemoveAsync(id);
            return NoContent();
        }
    }
}
