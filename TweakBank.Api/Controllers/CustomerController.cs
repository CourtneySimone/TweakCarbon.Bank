using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private readonly ILogger<CustomerController> _logger;
        ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionManager _transactionManager;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;


        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository,
            IMapper mapper, ITransactionManager transactionManager, ITransactionRepository transactionRepository,
            IAccountRepository accountRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        [Route("/AllCustomers")]
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetAllData();
        }


        [Route("/CreateCustomer")]
        [HttpPost]
        public void CreateCustomer(CreateCustomerDto customerDto)
        {

            var customer = _mapper.Map<Customer>(customerDto);

            _customerRepository.InsertRecord(customer);
            var newCustomerId = _customerRepository.GetCustomerId(customerDto.IdNumber);

            var account = new Account()
            {
                AccountType = customerDto.account.AccountType,
                Balance = customerDto.account.InitialDeposit,
                CustomerId = newCustomerId
            };
            _accountRepository.InsertRecord(account);

            _transactionManager.LogTransaction((int)Dto.DTO.TransactionType.CreateCustomer,
                transactingCustomerId: newCustomerId, amount: customerDto.account.InitialDeposit);
        }

        [Route("/GetTransactions")]
        [HttpPost]
        public IEnumerable<Transaction> GetTransactions(int customerId)
        {
            var all = _transactionRepository.GetAllData();

            var x = _transactionRepository.GetAllTransactionsForCustomer(customerId);

            return x;
        }


    }
}
