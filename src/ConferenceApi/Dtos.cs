namespace ConferenceApi
{
    public class Dtos
    {
        public record ConferenceDto(
            uint Id,
            string? Title,
            string? Description,
            string StartDate,
            string EndDate,
            string? OrganizationId,
            
            List<CategoryDto>? Categories,
            List<AddonDto>? Addons,
            List<CouponDto>? Coupons,
            LocationDto? Location,
            ContactDto? Contact,
            List<TermDto>? Terms
        );


        public record CategoryDto(
            Guid Id,
            string? Title,
            List<string>? DateOptions,
            List<PriceBookEntryDto>? PriceBookEntries
        );

        public record PriceBookEntryDto(
            Guid Id,
            string? IncomeLevel,
            List<PriceDto>? Prices
        );

        public record PriceDto(
            Guid Id,
            string? Type,
            decimal? PriceAmount,
            decimal Discount,
            string? Currency,
            DateTime ValidFrom,
            DateTime ValidUntil
        );

        public record AddonDto(
            Guid Id,
            string? Title,
            string? Description,
            decimal Amount
        );

        public record CouponDto(
            Guid Id,
            string? Code,
            string? DiscountType,
            decimal DiscountValue,
            List<string>? EmailList
        );

        public record LocationDto(
            Guid Id,
            string? Venue,
            AddressDto? Address
        );

        public record AddressDto(
            Guid Id,
            string? StreetAddress,
            string? City,
            string? ZipCode,
            string? Country
        );

        public record ContactDto(
            Guid Id,
            string? Email,
            string? Phone
        );

        public record TermDto(
            Guid Id,
            string? Title,
            string? Description,
            bool IsRequired
        );
        //conference create Dto
        public record ConferenceCreateDto(

            string? Title,
            string? Description,
            string StartDate,
            string EndDate,
            string? OrganizationId,
            List<CategoryCreateDto>? Categories,
            List<AddonCreateDto>? Addons,
            List<CouponCreateDto>? Coupons,
            LocationCreateDto? Location,
            ContactCreateDto? Contact,
            List<TermCreateDto>? Terms
        );

        public record CategoryCreateDto(
            string? Title,
            List<DateTime>? DateOptions,
            List<PriceBookEntryCreateDto>? PriceBookEntries
        );

        public record PriceBookEntryCreateDto(
            string? IncomeLevel,
            List<PriceCreateDto>? Prices
        );

        public record PriceCreateDto(
            string? Type,
            decimal? PriceAmount,
            decimal Discount,
            string? Currency,
            DateTime ValidFrom,
            DateTime ValidUntil
        );

        public record AddonCreateDto(
            string? Title,
            string? Description,
            decimal Amount
        );

        public record CouponCreateDto(
            string? Code,
            string? DiscountType,
            decimal DiscountValue,
            List<string>? EmailList
        );

        public record LocationCreateDto(
            string? Venue,
            AddressCreateDto? Address
        );

        public record AddressCreateDto(
            string? StreetAddress,
            string? City,
            string? ZipCode,
            string? Country
        );

        public record ContactCreateDto(
            string? Email,
            string? Phone
        );

        public record TermCreateDto(
            string? Title,
            string? Description,
            bool IsRequired
        );
        //conference update Dto
        public record ConferenceUpdateDto(
            string? Title,
            string? Description,
            string StartDate,
            string EndDate,
            string? OrganizationId,
            List<CategoryUpdateDto>? Categories,
            List<AddonUpdateDto>? Addons,
            List<CouponUpdateDto>? Coupons,
            LocationUpdateDto? Location,
            ContactUpdateDto? Contact,
            List<TermUpdateDto>? Terms
        );

        public record CategoryUpdateDto(
            Guid Id,
            string? Title,
            List<DateTime>? DateOptions,
            List<PriceBookEntryUpdateDto>? PriceBookEntries
        );

        public record PriceBookEntryUpdateDto(
            Guid Id,
            string? IncomeLevel,
            List<PriceUpdateDto>? Prices
        );

        public record PriceUpdateDto(
            Guid Id,
            string? Type,
            decimal? PriceAmount,
            decimal Discount,
            string? Currency,
            DateTime ValidFrom,
            DateTime ValidUntil
        );

        public record AddonUpdateDto(
            Guid Id,
            string? Title,
            string? Description,
            decimal Amount
        );

        public record CouponUpdateDto(
            Guid Id,
            string? Code,
            string? DiscountType,
            decimal DiscountValue,
            List<string>? EmailList
        );

        public record LocationUpdateDto(
            Guid Id,
            string? Venue,
            AddressUpdateDto? Address
        );

        public record AddressUpdateDto(
            Guid Id,
            string? StreetAddress,
            string? City,
            string? ZipCode,
            string? Country
        );

        public record ContactUpdateDto(
            Guid Id,
            string? Email,
            string? Phone
        );

        public record TermUpdateDto(
            Guid Id,
            string? Title,
            string? Description,
            bool IsRequired
        );

        // Config DTOs
        public record ConfigDto(
            uint Id,
            int OrganizationId,
            List<SectorDto>? Sectors
        );

        public record SectorDto(
            Guid Id,
            string Title
        );

        // Config Create DTOs
        public class ConfigCreateDto
        {
            public int OrganizationId { get; set; }
            public List<string> Sectors { get; set; } // Change to List<string>
        }

        public record SectorCreateDto(
            string Title
        );

        // Config Update DTOs
        public record ConfigUpdateDto(
            Guid Id,
            int OrganizationId,
            List<SectorUpdateDto>? Sectors
        );

        public record SectorUpdateDto(
            Guid Id,
            string Title
        );



    }
}