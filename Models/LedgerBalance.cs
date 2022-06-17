namespace LedgerBalanceService.Models
{
    public class LedgerBalance
    {
        public DateTime TimeStamp { get; set; }
        public long ClientId { get; set; }
        public double Balance { get; set; }
        public LedgerBalance(long clientId, double balance)
        {
            TimeStamp = DateTime.Now;
            ClientId = clientId; 
            Balance = balance;
        }
    }
}
