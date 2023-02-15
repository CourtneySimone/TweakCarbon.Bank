using TweakBank.Api.DTO;

namespace TweakBank.Logic
{
    public interface ITransactionManager
    {
        void TransferFunds(TransferDto transferDto);
        void LogTransaction(int type, int? transactingAccountId = null, int? transactingCustomerId = null,
                                    int? staffId = null, int? recipientAccountId = null, int? recipientCustomerId = null,
                                    string? reference = null, double? amount = null);

        void LogTransfer(int transactingAccountId, int recipientAccountId, double transferAmount);
    }
}