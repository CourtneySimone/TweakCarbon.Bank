using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakBank.GenericRepository;
using TweakBank.Models;

namespace TweakBank.Repository
{
    public class TransactionTypeRepository: GenericRepository<TransactionType>, ITransactionTypeRepository
    {
    }
}
