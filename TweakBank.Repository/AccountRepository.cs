using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public class AccountRepository :GenericRepository<Account>, IAccountRepository
    {
        public int GetCustomerId(int accountId)
        {
            var entity = dbEntity.Where(x => x.AccountId==accountId).First();
            return entity.CustomerId;
        }

        public double GetBalance(long accountId)
        {
            var balance =base.FindById(accountId).Balance;
            return balance;
        }
    }
}
