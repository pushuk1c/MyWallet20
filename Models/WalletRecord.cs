using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWallet.Models
{
    public class WalletRecord
    {
        public Guid id { get; set; }
        public DateTime date { get; set; }
        public Wallet? wallet { get; set; }
        public User? user { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal sum { get; set; } = 0;
        public string description { get; set; } = string.Empty;

    }
}
