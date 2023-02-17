using TweakBank.Api.DTO;

namespace TweakBank.Logic
{
    public interface ITransactionManager
    {
        (int recipientAccount, int? senderAccount) TransferFunds(TransferDto transferDto);
        void LogTransaction(int type, int? transactingAccountId = null, int? transactingCustomerId = null,
                                     int? recipientAccountId = null, int? recipientCustomerId = null,
                                    string? reference = null, double? amount = null);

        void LogTransfer(int transactingAccountId, double transferAmount, int? recipientAccountId = null);
    }
}