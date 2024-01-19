using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWallet.DTOs.WalletRecord
{
    public class AddWalletRecordDto
    {
        public DateTime date { get; set; }
        public Guid walletId { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal sum { get; set; } = 0;
        public string description { get; set; } = string.Empty;
    }
}
