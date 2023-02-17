using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public class BankAccountRepository :GenericRepository<BankAccount>, IBankAccountRepository
    {
        public int GetCustomerId(int accountId)
        {
            var entity = dbEntity.Where(x => x.BankAccountId==accountId).FirstOrDefault();
            return entity?.CustomerId??0;
        }

        public double GetBalance(long accountId)
        {
            var balance =base.FindById(accountId).Balance;
            return balance;
        }
    }
}
