namespace TweakBank.Api.DTO
{
    public class CreateBankForExistingCustomerAccountDto
    {
 
        public int customerId { get; set; }
        public double InitialDeposit { get; set; }
        public string AccountType { get; set; }

        
    }
}
