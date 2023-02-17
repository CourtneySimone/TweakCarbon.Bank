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
    public class BankAccountsController : ControllerBase
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionManager _transactionManager;
        private readonly ICustomerRepository _customerRepository;


        public BankAccountsController(IBankAccountRepository BankAccountRepository,
            IMapper mapper, ITransactionManager transactionManager, ICustomerRepository customerRepository)
        {
            _bankAccountRepository = BankAccountRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _customerRepository = customerRepository;
        }

        [Route("/AllAccounts")]
        [HttpGet]
        public ObjectResult GetAccounts()
        {
            try
            {
                var allAccounts = _bankAccountRepository.GetAllData();
                return new OkObjectResult(allAccounts);
            }
            catch (Exception ex)
            {
                return Problem(
              title: "Bad Input",
              detail: ex.Message,
              statusCode: StatusCodes.Status400BadRequest
              );

            }


        }

        [Route("/CreateAccount")]
        [HttpPost]
        public ObjectResult CreateAccount(CreateBankForExistingCustomerAccountDto BankAccountDto)
        {
            try
            {
                BankAccount account = _mapper.Map<BankAccount>(BankAccountDto);

                var existingCustomer = _customerRepository.FindById(BankAccountDto.customerId);
                if (existingCustomer == null)
                {
                    throw new Exception("Please create a customer first or add an existing customer id");
                }
                _bankAccountRepository.InsertRecord(account);
                _transactionManager.LogTransaction((int)Dto.DTO.TransactionType.CreateAccount, account.CustomerId, account.BankAccountId, account.CustomerId,
                    amount: account.Balance);

                return new OkObjectResult(account);
            }
            catch (Exception ex)
            {
                return Problem(
               title: "Bad Input",
               detail: ex.Message
               );
            }
        }

        [Route("/Balance")]
        [HttpGet]
        public ObjectResult GetBalance(int bankAccountId)
        {
            try
            {
                var customerId = _bankAccountRepository.GetCustomerId(bankAccountId);

                if (customerId == 0)
                {
                    throw new Exception("Please enter a valid bank account number");
                }

                _transactionManager.LogTransaction((int)Dto.DTO.TransactionType.GetBalance, transactingCustomerId: customerId, transactingAccountId: bankAccountId);
                return new OkObjectResult(_bankAccountRepository.GetBalance(bankAccountId));
            }
            catch (Exception ex)
            {
                return Problem(
               title: "Bad Input",
               detail: ex.Message,
               statusCode: StatusCodes.Status400BadRequest
               );
            }
        }

        [Route("/Transfer")]
        [HttpPost]
        public ObjectResult Transfer(TransferDto TransferDto)
        {
            try
            {
                (int senderAccount, int? recipientAccount) = _transactionManager.TransferFunds(TransferDto);

                _transactionManager.LogTransfer((int)senderAccount, TransferDto.TransferAmount, recipientAccount);

                return new ObjectResult(TransferDto);
            }
            catch (Exception ex)
            {
                return Problem(
              title: "Bad Input",
              detail: ex.Message
              );
            }

        }
    }
}
