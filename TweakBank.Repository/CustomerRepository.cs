using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
      public int GetCustomerId(int customerIdNumber)
        {
            return base.dbEntity.Where(x => x.IdNumber == customerIdNumber).First().CustomerId;

        }
    }
}
