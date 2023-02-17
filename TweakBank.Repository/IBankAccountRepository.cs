using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public interface IBankAccountRepository:IGenericRepository<BankAccount>
    {
        double GetBalance(long accountId);
        int GetCustomerId(int accountId);

    }
}