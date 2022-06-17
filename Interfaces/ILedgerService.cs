using LedgerBalanceService.Models;

namespace LedgerBalanceService.Interfaces
{
    public interface ILedgerService
    {
        void AddLedgerBalance(LedgerBalance ledgerBalance);
        double GetLedgerBalance(long clientId);
        object PrintLedgerBlockChain();
    }
}
