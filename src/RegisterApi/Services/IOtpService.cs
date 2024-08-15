namespace RegisterApi.Services;

public interface IOtpService
{
    Task SendOtpAsync(string email);
    Task<bool> ValidateOtpAsync(string email, string otp);
   
}
