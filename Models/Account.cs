using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TweakBank.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public double Balance { get; set; }
        public string AccountType { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

     
    }
}
