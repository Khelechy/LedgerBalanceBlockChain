using LedgerBalanceService.Interfaces;

namespace LedgerBalanceService.Models
{
    public class LedgerBalanceBlockChain : ILedgerService
    {
        private readonly int _proofOfWorkDifficulty = 2;
        private readonly double _miningReward = 10;
        private readonly long _minerAddress = 999;
        private List<LedgerBalance> _pendingLedgerBalances;
        public List<LedgerBalanceBlock> Chain { get; set; }

        public LedgerBalanceBlockChain()
        {
            _pendingLedgerBalances = new List<LedgerBalance>();
            Chain = new List<LedgerBalanceBlock> { CreateGenesisBlock() };
        }

        public void AddLedgerBalance(LedgerBalance ledgerBalance)
        {
            _pendingLedgerBalances.Add(ledgerBalance);
            //Simulate Mine after adding ledger
            MineLedgerBalanceBlock();
        }

        public void MineLedgerBalanceBlock()
        {
            LedgerBalance minerRewardTransaction = new LedgerBalance(_minerAddress, _miningReward);
            _pendingLedgerBalances.Add(minerRewardTransaction);
            LedgerBalanceBlock block = new LedgerBalanceBlock(DateTime.Now, _pendingLedgerBalances);
            block.MineLedgerBlock(_proofOfWorkDifficulty);
            block.PreviousHash = Chain.Last().Hash;
            Chain.Add(block);
            _pendingLedgerBalances = new List<LedgerBalance>();
        }

        public bool IsValidChain()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                LedgerBalanceBlock previousBlock = Chain[i - 1];
                LedgerBalanceBlock currentBlock = Chain[i];
                if (currentBlock.Hash != currentBlock.CreateHash())
                    return false;
                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;
            }
            return true;
        }

        private LedgerBalanceBlock CreateGenesisBlock()
        {
            List<LedgerBalance> ledgerBalances = new List<LedgerBalance> { new LedgerBalance(0, 0) };
            return new LedgerBalanceBlock(DateTime.Now, ledgerBalances, "0");
        }

        public double GetLedgerBalance(long clientId)
        {
            double balance = 0; 
            List<LedgerBalance> clientBalances = new List<LedgerBalance>();

            foreach (LedgerBalanceBlock block in Chain)
            {
                if(block.LedgerBalances != null)
                {
                    foreach (LedgerBalance ledgerBalance in block.LedgerBalances)
                    {
                        if(ledgerBalance.ClientId == clientId)
                        {
                            clientBalances.Add(ledgerBalance);
                        }
                    }
                }

            }
            clientBalances.Sort((x, y) => DateTime.Compare(x.TimeStamp, y.TimeStamp));
            balance = clientBalances.Last().Balance;
            return balance;
        }

        public object PrintLedgerBlockChain()
        {
            return Chain;
        }
    }
}
