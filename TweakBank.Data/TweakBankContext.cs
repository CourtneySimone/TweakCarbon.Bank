using TweakBank.Models;
using System.Data.Entity;

namespace TweakBank.Data
{
    public class TweakBankContext : DbContext
    {
        public TweakBankContext():base()
        {
        
        }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
 


    }
}
