using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TweakBank.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string? Reference { get; set; }
        public double? Amount { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("TransactingCustomer")]
        public int? TransactingCustomerId { get; set; }
        public virtual Customer? TransactingCustomer { get; set; }

        [ForeignKey("RecipientCustomer")]
        public int? RecipientCustomerId { get; set; }
        public virtual Customer? RecipientCustomer { get; set; }

        [ForeignKey("RecipientAccount")]
        public int? RecipientAccountId { get; set; }
        public virtual Account? RecipientAccount { get; set; }

        [ForeignKey("TransactingAccount")]
        public int? TransactingAccountId { get; set; }
        public virtual Account? TransactingAccount { get; set; }

        [Required]
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }

        [ForeignKey("StaffMember")]
        public int? StaffId { get; set; }
        public virtual Staff? StaffMember { get; set; }

    }
}
