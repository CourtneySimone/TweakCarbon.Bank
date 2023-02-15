using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public interface ITransactionRepository:IGenericRepository<Transaction>
    {
        List<Transaction> GetAllTransactionsForCustomer(int customerId);

    }
}