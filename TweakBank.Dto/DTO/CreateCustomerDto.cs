namespace TweakBank.Api.DTO
{
    public class CreateCustomerDto
    {
        public int? CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public int IdNumber { get; set; }
        public CreateAccountDto account { get; set; }
    }
}
