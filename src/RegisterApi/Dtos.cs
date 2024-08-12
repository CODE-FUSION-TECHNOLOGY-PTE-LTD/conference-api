using System.ComponentModel.DataAnnotations;
using CommonLib.Models;
using Microsoft.AspNetCore.Http;

namespace RegisterApi;

public class Dtos
{
    public record UserRegisterDto(int title, string first_name, string second_name, string surname, string gender, string age_range,
         string email, string job_title, string country_id, string city, string address, string postal_code, int organisation_id, string telephone, string phone, string? sectorType, IFormFile? document);
    public record UserUpdateDto
    {
        public int? title { get; set; }
        public string? first_name { get; set; }
        public string? second_name { get; set; }
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
    }
    public record UserLoginDtos
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }

}

