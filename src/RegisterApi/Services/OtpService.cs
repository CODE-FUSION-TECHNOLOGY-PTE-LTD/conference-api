using System.Collections.Concurrent;
using CommonLib;
using CommonLib.MySql;
using Microsoft.EntityFrameworkCore;

namespace RegisterApi.Services
{
    public class OtpService : IOtpService
    {
        private readonly ILogger<OtpService> _logger;
        private readonly ConcurrentDictionary<string, string> _otpStore = new();

        private readonly EmailService emailService;

        private readonly IdentityDbContext identityDbContext;

        public OtpService(ILogger<OtpService> logger, IdentityDbContext identityDbContext, EmailService emailService)
        {
            this.identityDbContext = identityDbContext;
            _logger = logger;
            this.emailService = emailService;
        }



        public async Task SendOtpAsync(string email)
        {
            var user = await identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);


            if (user != null)
            {
                var otp = new Random().Next(100000, 999999).ToString();
                user.Otp = otp;
                await identityDbContext.SaveChangesAsync(); // Save the OTP to the database
                await emailService.SendEmailAsync(email, "OTP", $"Your OTP is: {otp}");
                _logger.LogInformation($"OTP sent to {email}: {otp}");
            }
            else
            {
                _logger.LogWarning($"User not found with email: {email}");
            }

        }

        public async Task<bool> ValidateOtpAsync(string email, string otp)
        {
            var user = await identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && user.Otp == otp)
            {
                user.Otp = null;
                await identityDbContext.SaveChangesAsync();
                return true;
            }
            _logger.LogWarning($"Invalid OTP for {email}");
            return false;
        }
    }
}
