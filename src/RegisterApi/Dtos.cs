namespace RegisterApi;

public class Dtos
{
    public record UserRegisterDto(int title, string first_name, string second_name, string surname, string gender, string age_range,
         string email, string job_title, string country_id, string city, string address, string postal_code, int organisation_id, string telephone, string phone, string? sectorType, IFormFile? document);
}
