
namespace RegisterApi;

public class Dtos
{
    public record UserRegisterDto
    {
        public int title { get; set; }
        public string? first_name { get; set; }
        public string? surname { get; set; }
        public int? gender { get; set; }
        public int? age_range { get; set; }
        public string? email { get; set; }
        public string? job_title { get; set; }
        public int? country_id { get; set; }
        public string? city { get; set; }
        public string? address { get; set; }
        public string? postal_code { get; set; }
        public string? alternative_email { get; set; }
        public int? organisation_id { get; set; }
        public int? department { get; set; }
        public string? phone { get; set; }
        public string? sectorType { get; set; }
        public string? document { get; set; }
        public string? password { get; set; }
    }
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
        public string? document { get; set; }
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


    public class UserMessageDto
    {
        public uint Id { get; set; }
        public string? Email { get; set; }
        public string? Secter { get; set; }
        public string? Document { get; set; }

    }

}

