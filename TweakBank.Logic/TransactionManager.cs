using System.Reflection.Metadata.Ecma335;
using TweakBank.Api.DTO;
using TweakBank.Dto.DTO;
using TweakBank.Models;
using TweakBank.Repository;

namespace TweakBank.Logic
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IBankAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        public TransactionManager(IBankAccountRepository accountRepo, ITransactionRepository transactionRepo)
        {
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
        }
        public (int recipientAccount,int? senderAccount) TransferFunds(TransferDto transferDto)
        {
            var senderAccount = _accountRepo.FindById(transferDto.SenderBankAccountId);
            var recipientAccount = _accountRepo.FindById(transferDto.RecipientBankAccountId);

            if(senderAccount == null || senderAccount.Balance<transferDto.TransferAmount)
            {
                throw (new Exception("sender account cannot be found or has an insufficient balance"));
            }

            senderAccount.Balance = senderAccount.Balance - transferDto.TransferAmount;
            _accountRepo.Update(senderAccount);


            if (recipientAccount != null)
            {
                recipientAccount.Balance = recipientAccount.Balance + transferDto.TransferAmount;
                _accountRepo.Update(recipientAccount);
                return (recipientAccount.BankAccountId, senderAccount.BankAccountId);
            }

            return (senderAccount.BankAccountId, null);


        }

        public void LogTransaction(int type, int? transactingBankAccountId = null, int? transactingCustomerId = null,
                                     int? recipientAccountId = null, int? recipientCustomerId = null,
                                    string? reference = null, double? amount = null)
        {
            var transaction = new Transaction()
            {
                Amount = amount,
                Date = DateTime.Now,
                RecipientBankAccountId = recipientAccountId,
                TransactingBankAccountId = transactingBankAccountId,
                RecipientCustomerId = recipientCustomerId,
                TransactingCustomerId = transactingCustomerId,
                Reference = reference,
                TransactionTypeId = (int)type,
            };

            _transactionRepo.InsertRecord(transaction);
        }

        public void LogTransfer(int transactingAccountId, double transferAmount,int? recipientAccountId = null)
        {
            int? recipientCustomerId = null;
            if (recipientAccountId != null)
            {
                 recipientCustomerId = _accountRepo.GetCustomerId((int)recipientAccountId);

            }
            var transactingCustomerId = _accountRepo.GetCustomerId(transactingAccountId);

            LogTransaction((int)Dto.DTO.TransactionType.Transfer,transactingCustomerId: transactingCustomerId,transactingBankAccountId: transactingAccountId,
                recipientAccountId: recipientAccountId, recipientCustomerId: recipientCustomerId, amount: transferAmount);
           
            LogTransaction((int)Dto.DTO.TransactionType.recievedFunds, transactingCustomerId: transactingCustomerId, transactingBankAccountId: transactingAccountId,
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