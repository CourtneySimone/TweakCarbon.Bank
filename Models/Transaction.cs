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

        [ForeignKey("RecipientBankAccount")]
        public int? RecipientBankAccountId { get; set; }
        public virtual BankAccount? RecipientBankAccount { get; set; }

        [ForeignKey("TransactingBankAccount")]
        public int? TransactingBankAccountId { get; set; }
        public virtual BankAccount? TransactingBankAccount { get; set; }

        [Required]
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }


    }
}
