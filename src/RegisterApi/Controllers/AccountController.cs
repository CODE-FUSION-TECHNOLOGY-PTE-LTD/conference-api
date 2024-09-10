
using System.IdentityModel.Tokens.Jwt;
using AuthManager;
using AuthManager.Models;
using CommonLib;
using CommonLib.Models;
using CommonLib.MySql;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegisterApi.fileUpload.services;
using RegisterApi.Services;

using static RegisterApi.Dtos;

namespace RegisterApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{

    private readonly MySqlRepository<User> repository;

    private readonly MySqlDbContext mySqlDbContext;

    private readonly JwtTokenHandler _jwtTokenHandler;

    private readonly ManageFile manageFile;

    private readonly IOtpService otpService;

    private readonly IBus bus;

    public AccountController(IOtpService otpService, JwtTokenHandler jwtTokenHandler, IBus bus, ManageFile manageFile, MySqlRepository<User> repository, MySqlDbContext mySqlDbContext)
    {
        this.manageFile = manageFile;
        this.repository = repository;
        this.bus = bus;
        this.mySqlDbContext = mySqlDbContext;
        _jwtTokenHandler = jwtTokenHandler;
        this.otpService = otpService;
    }


    [HttpPost("register")]
    public async Task<ActionResult<User>> PostAsync(UserRegisterDto userDto)
    {
        if (!uint.TryParse(userDto.country_id.ToString(), out uint countryId))
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
            Surname = userDto.surname,
            Gender = userDto.gender!.ToString(),
            AgeRange = userDto.age_range!.ToString(),
            Email = userDto.email,
            JobTitle = userDto.job_title,
            CountryId = userDto.country_id!.ToString(),
            City = userDto.city,
            AddressLine1 = userDto.address,
            PoCode = userDto.postal_code,
            AlternativeEmail = userDto.alternative_email,
            OrganisationId = userDto.organisation_id,
            Mobile = userDto.phone,
            DepartmentId = userDto.department,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.password),
        };


        await repository.CreateAsync(user);

        var userMessage = new UserMessageDto
        {
            Id = user.Id,
            Email = user.Email,
            Secter = userDto.sectorType,
            Document = userDto.document,

        };

        //send
        var url = new Uri("rabbitmq:localhost/Account_register");
        var endPoint = await bus.GetSendEndpoint(url);
        await endPoint.Send(userMessage);

        //token
        var tokenHandler = new JwtSecurityTokenHandler();
        var authReqest = new AuthenticationRequest
        {
            UserName = user.Email,
            Password = userDto.password
        };

        var token = await _jwtTokenHandler.GenerateJSONWebTokenAsync(authReqest);

        if (token == null)
        {
            return Unauthorized();
        }

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
        {

            user.Id,
            token.JwtToken,
            token.ExpireIn,
            country.WorldBankIncomeGroup,

        });


    }


    [HttpGet("register/{id}")]
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





        var user = new User
        {

            Title = userDto.title,
            FirstName = userDto.first_name,

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
        };
        await repository.UpdateAsync(user);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpDelete("register/{id}")]
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
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Invalid login request");
        }

        // Fetch the user from the database
        var user = await repository.LoginAsync(request.UserName, request.Password);
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        // Verify the password
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!isPasswordValid)
        {
            return Unauthorized("Invalid username or password");
        }

        // Generate JWT token
        var authResponse = await _jwtTokenHandler.GenerateJSONWebTokenAsync(new AuthenticationRequest
        {
            UserName = request.UserName,
            Password = request.Password
        });

        if (authResponse == null)
        {
            return BadRequest("Failed to generate token");
        }

        return Ok(authResponse);
    }
    [HttpPost("request-send-forgotpassword")]
    public async Task<IActionResult> RequestSendForgotPassword([FromBody] RequestResetDto request)
    {
        var user = await repository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        await otpService.SendOtpAsync(request.Email); // Send OTP to user's email
        return Ok("OTP sent to email.");
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] VerifyOtpDto request)
    {
        var isOtpValid = await otpService.ValidateOtpAsync(request.Email, request.Otp);

        if (!isOtpValid)
        {
            return BadRequest("Invalid OTP.");
        }

        var user = await repository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        user.Password = HashPassword(request.NewPassword); // Hash the new password before saving
        await repository.UpdateAsync(user);
        Console.WriteLine($"Password reset successful for email: {request.Email}");

        return Ok("Password reset successful.");
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }




}
