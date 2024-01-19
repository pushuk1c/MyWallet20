namespace MyWallet.Models
{
    public class User
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public byte[] passwordHash { get; set; } = new byte[0];
        public byte[] passwordSalt { get; set; } = new byte[0];
        public List<Wallet> wallets { get; set; }
        public List<WalletRecord> walletRecords { get; set; }

    }
}
