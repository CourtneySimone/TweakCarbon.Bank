using System.ComponentModel.DataAnnotations;

namespace TweakBank.Models
{
    public class TransactionType
    {
        [Key]
        public int TransactionTypeId { get; set; }
        public string TransactionName { get; set; }
    }
}