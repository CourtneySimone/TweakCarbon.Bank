using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweakBank.Dto.DTO
{
    public class CreateBankForNewCustomerAccountDto
    {
        public double InitialDeposit { get; set; }
        public string AccountType { get; set; }
    }
}
