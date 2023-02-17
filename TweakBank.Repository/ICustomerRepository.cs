using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        int GetCustomerId(long customerIdNumber);
    }
}