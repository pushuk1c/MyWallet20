namespace MyWallet.Models
{
    public class Wallet
    {
        public Guid id { get; set; }    
        public string name { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public User? user { get; set; }
        public List<WalletRecord> walletRecords { get; set; }
    }
}
