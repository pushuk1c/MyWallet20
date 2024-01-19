using MyWallet.DTOs.WalletRecord;
using MyWallet.Models;

namespace MyWallet
{
    public class AutoMapperProfile : Profile    
    {
        public AutoMapperProfile()
        {
            CreateMap<Wallet, GetWalletDto>();
            CreateMap<AddWalletDto, Wallet>();
            CreateMap<UpdateWalletDto, Wallet>();
            CreateMap<AddWalletRecordDto, WalletRecord>();
            CreateMap<WalletRecord, GetWalletRecordDto>();

        }

    }
}
