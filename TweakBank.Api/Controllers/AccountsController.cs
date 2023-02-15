using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TweakBank.Api.DTO;
using TweakBank.Logic;
using TweakBank.Models;
using TweakBank.Repository;

namespace TweakBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionManager _transactionManager;


        public AccountsController(ILogger<AccountsController> logger, IAccountRepository accountRepository,
            IMapper mapper, ITransactionManager transactionManager)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
        }

        [Route("/AllAccounts")]
        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAllData();
        }

        [Route("/CreateAccount")]
        [HttpPost]
        public void CreateAccount(CreateAccountDto acccountDto)
        {
            Account account = _mapper.Map<Account>(acccountDto);
            _accountRepository.InsertRecord(account);
            _transactionManager.LogTransaction((int)Dto.DTO.TransactionType.CreateAccount,account.CustomerId, account.AccountId, account.CustomerId,
                amount: account.Balance);
        }

        [Route("/Balance")]
        [HttpGet]
        public double GetBalance(int accountId, int? staffId)
        {
            var customerId = _accountRepository.GetCustomerId(accountId);

            _transactionManager.LogTransaction((int)Dto.DTO.TransactionType.GetBalance,transactingCustomerId: customerId, transactingAccountId: accountId,staffId:staffId );
            return _accountRepository.GetBalance(accountId);

        }

        [Route("/Transfer")]
        [HttpPost]
        public void Transfer(TransferDto TransferDto)
        {
            _transactionManager.TransferFunds(TransferDto);


            _transactionManager.LogTransfer(TransferDto.SenderAccountId, TransferDto.RecipientAccountId, 
                TransferDto.TransferAmount);


        }
    }
}
