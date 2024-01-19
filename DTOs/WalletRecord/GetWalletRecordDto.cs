using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyWallet.DTOs.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWallet.DTOs.WalletRecord
{
    public class GetWalletRecordDto
    {
        public Guid id { get; set; }
        public DateTime date { get; set; }
        public GetWalletDto? wallet { get; set; }
        public UserLoginDto? user{ get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal sum { get; set; } = 0;
        public string description { get; set; }
    }
}
