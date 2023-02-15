using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public List<Transaction> GetAllTransactionsForCustomer(int customerId)
        {
            return base.dbEntity.Where(x => x.TransactingCustomerId == customerId || x.RecipientCustomerId == customerId).ToList();
        }
    }
}
