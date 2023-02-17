using System.ComponentModel.DataAnnotations;
using TweakBank.Dto.DTO;

namespace TweakBank.Api.DTO
{
    public class CreateCustomerDto
    {
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
        public CreateBankForNewCustomerAccountDto BankAccount { get; set; }
    }
}
