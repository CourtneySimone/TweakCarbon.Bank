using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweakBank.Dto.DTO
{
    public enum TransactionType
    {
        Transfer = 1, 
        Withdraw,
        GetBalance, 
        GetHistory,
        CreateCustomer,
        Deposit,
        SendEWallet,
        GetAllAccounts,
        GetCustomers, 
        recievedFunds,
        CreateAccount

    }
}
