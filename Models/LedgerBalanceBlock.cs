using System.Security.Cryptography;
using System.Text;

namespace LedgerBalanceService.Models
{
    public class LedgerBalanceBlock
    {
        private readonly DateTime _timeStamp;
        private long _nonce;
        public string PreviousHash { get; set; }

        public List<LedgerBalance> LedgerBalances { get; set; }

        public string Hash { get; private set; }
        public LedgerBalanceBlock(DateTime timeStamp, List<LedgerBalance> ledgerBalances, string previousHash = "")
        {
            _timeStamp = timeStamp;
            _nonce = 0;
            LedgerBalances = ledgerBalances;
            PreviousHash = previousHash;
            Hash = CreateHash();
        }

        public string CreateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = PreviousHash + _timeStamp + LedgerBalances + _nonce;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Encoding.Default.GetString(bytes);
            }
        }

        public void MineLedgerBlock(int proofOfWorkDifficulty)
        {
            string hashValidationTemplate = new String('0', proofOfWorkDifficulty);

            while (Hash.Substring(0, proofOfWorkDifficulty) != hashValidationTemplate)
            {
                _nonce++;
                Hash = CreateHash();
            }
            Console.WriteLine("Block with HASH={0} successfully mined!", Hash);
        }
    }
}
