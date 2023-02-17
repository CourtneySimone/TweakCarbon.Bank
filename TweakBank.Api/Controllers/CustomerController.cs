using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TweakBank.Api.DTO;
using TweakBank.Logic;
using TweakBank.Models;
using TweakBank.Repository;

namespace TweakBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionManager _transactionManager;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBankAccountRepository _accountRepository;


        public CustomerController(ICustomerRepository customerRepository,
            IMapper mapper, ITransactionManager transactionManager, ITransactionRepository transactionRepository,
            IBankAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        [Route("/AllCustomers")]
        [HttpGet]
        public ObjectResult GetCustomers()
        {
            try
            {
                var allAccounts = _customerRepository.GetAllData();
                return new OkObjectResult(allAccounts);
            }
            catch (Exception ex)
            {
                return Problem(
              title: "Bad Input - customers not found",
              detail: ex.Message
              );

            }
        }


        [Route("/CreateCustomer")]
        [HttpPost]
        public ObjectResult CreateCustomer(CreateCustomerDto customerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);

                if(customer.IdNumber == null || customer.IdNumber ==0)
                {
                    throw new ArgumentNullException("Please insert an Id number"); 
                }

                _customerRepository.InsertRecord(customer);
                var newCustomerId = _customerRepository.GetCustomerId(customerDto.IdNumber);

                var account = new BankAccount()
                {
                    AccountType = customerDto.BankAccount.AccountType,
                    Balance = customerDto.BankAccount.InitialDeposit,
                    CustomerId = newCustomerId
                };
                _accountRepository.InsertRecord(account);

                _transactionManager.LogTransaction((int)Dto.DTO.TransactionType.CreateCustomer,
                    transactingCustomerId: newCustomerId, amount: customerDto.BankAccount.InitialDeposit);
                return new OkObjectResult(customer);

            }
            catch (Exception ex)
            {
                return Problem(
              title: "Bad Input - customers not found",
              detail: ex.Message
              );

            }

        }

        [Route("/GetTransactions")]
        [HttpPost]
        public ObjectResult GetTransactions(int customerId)
        {
            try
            {
                if(customerId == null|| customerId==0)
                {
                    throw new ArgumentNullException("customer ID cannot be null or 0 ");
                }
               var transactions = _transactionRepository.GetAllTransactionsForCustomer(customerId);
                return new ObjectResult(transactions);
            }
            catch (Exception ex)
            {
                return Problem(
              title: "Bad Input - customers not found",
              detail: ex.Message
              );

            }
        }


    }
}
