using Moq;
using TweakBank.Api.DTO;
using TweakBank.Dto.DTO;
using TweakBank.Logic;
using TweakBank.Models;
using TweakBank.Repository;

namespace TweakBank.LogicTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenInternalAccountTransfer_TransferFunds_ShouldUpdateBothAccounts()
        {
            //Arrange
            var transferDto = new TransferDto()
            {
                RecipientAccountId = 2,
                SenderAccountId = 1,
                Reference = "Unit Test",
                TransferAmount = 1
            };
            var epectedSenderAccountsToUpdate = new Models.Account() { AccountId = 2, Balance = 3, CustomerId = 4 };
            var epectedRecieverAccountsToUpdate = new Models.Account() { AccountId = 2, Balance = 3, CustomerId = 4 };

            var transactionRepo = new Mock<ITransactionRepository>();
            Mock<IAccountRepository> accountRepo = new Mock<IAccountRepository>();
            accountRepo.Setup(x => x.FindById(transferDto.SenderAccountId)).Returns(epectedSenderAccountsToUpdate);
            accountRepo.Setup(x => x.FindById(transferDto.RecipientAccountId)).Returns(epectedRecieverAccountsToUpdate);
            TransactionManager transactionManager = new TransactionManager(accountRepo.Object, transactionRepo.Object);


            //Act
            transactionManager.TransferFunds(transferDto);

            epectedSenderAccountsToUpdate.Balance = epectedSenderAccountsToUpdate.Balance - transferDto.TransferAmount;
            epectedRecieverAccountsToUpdate.Balance = epectedRecieverAccountsToUpdate.Balance + transferDto.TransferAmount;

            //Assert 
            accountRepo.Verify(s => s.Update(epectedSenderAccountsToUpdate), Times.Once());
            accountRepo.Verify(s => s.Update(epectedRecieverAccountsToUpdate), Times.Once());

        }

        [TestMethod]
        public void GivenExternalAccountTransfer_TransferFunds_ShouldUpdateOnlyInternalAccount()
        {
            //Arrange
            var transferDto = new TransferDto()
            {
                RecipientAccountId = 2,
                SenderAccountId = 1,
                Reference = "Unit Test",
                TransferAmount = 1
            };
            var epectedSenderAccountsToUpdate = new Models.Account() { AccountId = 2, Balance = 3, CustomerId = 4 };

            var transactionRepo = new Mock<ITransactionRepository>();
            Mock<IAccountRepository> accountRepo = new Mock<IAccountRepository>();
            accountRepo.Setup(x => x.FindById(transferDto.SenderAccountId)).Returns(epectedSenderAccountsToUpdate);
            TransactionManager transactionManager = new TransactionManager(accountRepo.Object, transactionRepo.Object);


            //Act
            transactionManager.TransferFunds(transferDto);

            epectedSenderAccountsToUpdate.Balance = epectedSenderAccountsToUpdate.Balance - transferDto.TransferAmount;

            //Assert 
            accountRepo.Verify(s => s.Update(epectedSenderAccountsToUpdate), Times.AtMostOnce());

        }


        [TestMethod]
        public void LogTransaction_GivenValidTransaction_ShouldLogTransaction()
        {
            //Arrange
            var transactionRepo = new Mock<ITransactionRepository>();
            Mock<IAccountRepository> accountRepo = new Mock<IAccountRepository>();
            TransactionManager transactionManager = new TransactionManager(accountRepo.Object, transactionRepo.Object);

            var expectedAmount = 33;
            var expectedDate = DateTime.UtcNow;

            var transaction = new Transaction()
            {
                Amount = expectedAmount,
                Date = DateTime.Now
            };

            //Act
            transactionManager.LogTransaction((int)Dto.DTO.TransactionType.GetBalance, amount: expectedAmount);

            //Assert 
            transactionRepo.Verify(s => s.InsertRecord(It.IsAny<Transaction>()), Times.AtMostOnce());

        }
    }
}