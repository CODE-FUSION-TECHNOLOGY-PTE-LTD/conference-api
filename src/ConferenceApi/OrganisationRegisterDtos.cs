using System;
using System.Collections.Generic;

namespace ConferenceApi.OrganisationRegisterDtos // Use a proper namespace here
{
    public class OrganisationDto
    {
        public uint Id { get; set; }
        public string OrganisationName { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string AlternativeEmail { get; set; }
        public string ZopCode { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public List<MemberDto> Members { get; set; } = new List<MemberDto>();
    }

    public class MemberDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
        public decimal Amount { get; set; }
    }

    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Amount { get; set; }
    }
}
