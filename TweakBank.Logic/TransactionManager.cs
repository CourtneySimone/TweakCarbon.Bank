using TweakBank.Api.DTO;
using TweakBank.Dto.DTO;
using TweakBank.Models;
using TweakBank.Repository;

namespace TweakBank.Logic
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        public TransactionManager(IAccountRepository accountRepo, ITransactionRepository transactionRepo)
        {
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
        }
        public void TransferFunds(TransferDto transferDto)
        {
            var senderAccount = _accountRepo.FindById(transferDto.SenderAccountId);
            var recipientAccount = _accountRepo.FindById(transferDto.RecipientAccountId);

            senderAccount.Balance = senderAccount.Balance - transferDto.TransferAmount;
            _accountRepo.Update(senderAccount);

            if (recipientAccount != null)
            {
                recipientAccount.Balance = recipientAccount.Balance + transferDto.TransferAmount;
                _accountRepo.Update(recipientAccount);
            }

        }

        public void LogTransaction(int type, int? transactingAccountId = null, int? transactingCustomerId = null,
                                    int? staffId = null, int? recipientAccountId = null, int? recipientCustomerId = null,
                                    string? reference = null, double? amount = null)
        {
            var transaction = new Transaction()
            {
                Amount = amount,
                Date = DateTime.Now,
                RecipientAccountId = recipientAccountId,
                TransactingAccountId = transactingAccountId,
                RecipientCustomerId = recipientCustomerId,
                TransactingCustomerId = transactingCustomerId,
                Reference = reference,
                TransactionTypeId = (int)type,
                StaffId = staffId
            };

            _transactionRepo.InsertRecord(transaction);
        }

        public void LogTransfer(int transactingAccountId,int recipientAccountId, double transferAmount)
        {
            var transactingCustomerId = _accountRepo.GetCustomerId(transactingAccountId);
            var recipientCustomerId = _accountRepo.GetCustomerId(recipientAccountId);

            LogTransaction((int)Dto.DTO.TransactionType.Transfer,transactingCustomerId: transactingCustomerId,transactingAccountId: transactingAccountId,
                recipientAccountId: recipientAccountId, recipientCustomerId: recipientCustomerId, amount: transferAmount);
           
            LogTransaction((int)Dto.DTO.TransactionType.recievedFunds, transactingCustomerId: transactingCustomerId, transactingAccountId: transactingAccountId,
                recipientAccountId: recipientAccountId, recipientCustomerId: recipientCustomerId, amount: transferAmount);
        }

        public void WithdrawFunds()
        {
            //TODO
        }

        public void DepositFunds()
        {
            //TODO
        }

        public void RecieveFunds()
        {
            //TODO
        }
    }
}