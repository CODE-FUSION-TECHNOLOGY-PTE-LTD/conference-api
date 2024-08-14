using System.Collections.Concurrent;
using CommonLib.MySql;
using Microsoft.EntityFrameworkCore;

namespace RegisterApi.Services
{
    public class OtpService : IOtpService
    {
        private readonly ILogger<OtpService> _logger;
        private readonly ConcurrentDictionary<string, string> _otpStore = new();

        private readonly MySqlDbContext mySqlDbContext;

        public OtpService(ILogger<OtpService> logger, MySqlDbContext mySqlDbContext)
        {
            this.mySqlDbContext = mySqlDbContext;
            _logger = logger;
        }



        public async Task SendOtpAsync(string email)
        {
            var user = await mySqlDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);


            if (user != null)
            {
                var otp = new Random().Next(100000, 999999).ToString();
                user.Otp = otp;
                await mySqlDbContext.SaveChangesAsync(); // Save the OTP to the database

                _logger.LogInformation($"OTP sent to {email}: {otp}");
            }
            else
            {
                _logger.LogWarning($"User not found with email: {email}");
            }

        }

        public async Task<bool> ValidateOtpAsync(string email, string otp)
        {
            var user = await mySqlDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && user.Otp == otp)
            {
                user.Otp = null;
                await mySqlDbContext.SaveChangesAsync();
                return true;
            }
            _logger.LogWarning($"Invalid OTP for {email}");
            return false;
        }
    }
}
