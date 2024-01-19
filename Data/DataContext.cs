

using MyWallet.Models;

namespace MyWallet.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<User> Users => Set<User>();
        public DbSet<WalletRecord> WalletRecords => Set<WalletRecord>();

    }
}
