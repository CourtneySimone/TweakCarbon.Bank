namespace TweakBank.Api.DTO
{
    public class TransferDto
    {
        public int SenderBankAccountId { get; set; }
        public int? RecipientBankAccountId { get; set; }
        public double TransferAmount { get; set; } 
= 0;    public string Reference { get; set; }
    }
}
