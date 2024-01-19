using MyWallet.DTOs.WalletRecord;
using MyWallet.Models;

namespace MyWallet.Services.WalletRecordServices
{
    public interface IWalletRecordServices
    {       
        Task<ServiceResponse<GetWalletRecordDto>> AddWalletRecord(AddWalletRecordDto newWalletRecord);
        Task<ServiceResponse<List<GetWalletRecordDto>>> GetAllWalletRecords();


    }
}
