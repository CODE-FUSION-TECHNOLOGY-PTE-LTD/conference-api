
namespace RegisterApi;

public class Dtos
{
    public record UserRegisterDto(int title, string first_name, string surname, string? gender, string age_range,
         string email, string job_title, string country_id, string city, string address, string postal_code, int? organisation_id, string? telephone, string phone, string? sectorType, IFormFile? document, string? password);
    public record UserUpdateDto
    {
        public int? title { get; set; }
        public string? first_name { get; set; }
        public string? surname { get; set; }
        public string? gender { get; set; }
        public string? age_range { get; set; }
        public string? job_title { get; set; }
        public string? country_id { get; set; }
        public string? city { get; set; }
        public string? address { get; set; }
        public string? postal_code { get; set; }
        public int? organisation_id { get; set; }
        public string? telephone { get; set; }
        public string? phone { get; set; }
        public string? sectorType { get; set; }
        public IFormFile? document { get; set; }
        public string? password { get; set; }
    }
    public class RequestResetDto
    {
        public string Email { get; set; }
    }

    public class VerifyOtpDto
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; }
    }

}

