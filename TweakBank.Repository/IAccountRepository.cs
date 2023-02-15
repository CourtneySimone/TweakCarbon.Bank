using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public interface IAccountRepository:IGenericRepository<Account>
    {
        double GetBalance(long accountId);
        int GetCustomerId(int accountId);

    }
}