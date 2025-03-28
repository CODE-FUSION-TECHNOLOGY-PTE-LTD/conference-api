
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthManager.Models;
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
public class AccountController(IOtpService otpService, JwtTokenHandler jwtTokenHandler, IBus bus, ManageFile manageFile, MySqlRepository<User> repository, MySqlDbContext mySqlDbContext) : ControllerBase
{

    private readonly MySqlRepository<User> repository = repository;

    private readonly MySqlDbContext mySqlDbContext = mySqlDbContext;

    private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;

    private readonly ManageFile manageFile = manageFile;

    private readonly IOtpService otpService = otpService;

    private readonly IBus bus = bus;

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

        // Check if the user with this email already exists
        var existingUser = await mySqlDbContext.Users
            .Where(u => u.Email == userDto.email)
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            // If the user already exists, update the existing user with the new information
            existingUser.Title = userDto.title;
            existingUser.FirstName = userDto.first_name;
            existingUser.Surname = userDto.surname;
            existingUser.Gender = userDto.gender!.ToString();
            existingUser.AgeRange = userDto.age_range!.ToString();
            existingUser.JobTitle = userDto.job_title;
            existingUser.CountryId = userDto.country_id!.ToString();
            existingUser.City = userDto.city;
            existingUser.AddressLine1 = userDto.address;
            existingUser.PoCode = userDto.postal_code;
            existingUser.AlternativeEmail = userDto.alternative_email;
            existingUser.OrganisationId = userDto.organisation_id;
            existingUser.Mobile = userDto.phone;
            existingUser.DepartmentId = userDto.department;
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(userDto.password);

            // Update the user in the database
            await repository.UpdateAsync(existingUser);

            // Send message to RabbitMQ
            var userMessage = new UserMessageDto
            {
                Id = existingUser.Id,
                Email = existingUser.Email,
                Secter = userDto.sectorType,
                Document = userDto.document,
            };

            var url = new Uri("rabbitmq:localhost/Account_register");
            var endPoint = await bus.GetSendEndpoint(url);
            await endPoint.Send(userMessage);

            return Ok(new
            {
                status = 200,
                message = "User updated successfully",
                user = existingUser,
                country.WorldBankIncomeGroup
            });
        }
        else
        {
            // If the user does not exist, create a new user
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

            // Create the new user in the database
            await repository.CreateAsync(user);

            // Send message to RabbitMQ
            var userMessage = new UserMessageDto
            {
                Id = user.Id,
                Email = user.Email,
                Secter = userDto.sectorType,
                Document = userDto.document,
            };

            var url = new Uri("rabbitmq:localhost/Account_register");
            var endPoint = await bus.GetSendEndpoint(url);
            await endPoint.Send(userMessage);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
            {
                user.Id,
                country.WorldBankIncomeGroup
            });
        }
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

        

        return new ObjectResult(new 
        {
            status = 200,
            message = "Login Successfully",
            token = authResponse.JwtToken,
            UserId = user.Id,
         
        }) 
        {
            StatusCode = 200,
        };
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

    [HttpPost("pre-register")]
    public async Task<ActionResult<User>> PreRegister(UserPreRegisterDto userPreRegisterDto){
       // Check if the user with this email already exists
        var existingUser = await mySqlDbContext.Users
            .Where(u => u.Email == userPreRegisterDto.email)
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            // If the user already exists, return a conflict status with an appropriate message
            return Conflict(new { status = 409, message = "User with this email already exists. Registration not allowed." });
        }

        var user = new User
        {         
            Email = userPreRegisterDto.email,        
            Password = BCrypt.Net.BCrypt.HashPassword(userPreRegisterDto.password),
        };
          var userMessage = new UserMessageDto
        {
            Id = user.Id,
            Email = user.Email,          
        };

        //send
        var url = new Uri("rabbitmq:localhost/Account_register");
        var endPoint = await bus.GetSendEndpoint(url);
        await endPoint.Send(userMessage);
        await repository.CreateAsync(user);
        return new ObjectResult(new { status = 201, message = "User Registered Successfully"}) 
        {
            StatusCode = 201

        };
    }

    




    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }




}