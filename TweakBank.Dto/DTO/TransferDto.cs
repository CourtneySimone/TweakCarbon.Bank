namespace TweakBank.Api.DTO
{
    public class TransferDto
    {
        public int SenderAccountId { get; set; }
        public int RecipientAccountId { get; set; }
        public double TransferAmount { get; set; } 
= 0;    public string Reference { get; set; }
    }
}
