using MyWallet.Models;

namespace MyWallet.Services.WalletServices
{
    public interface IWalletServices
    {
        Task<ServiceResponse<GetWalletDto>> GetWalletById(string id);
        Task<ServiceResponse<List<GetWalletDto>>> GetAllWallets();
        Task<ServiceResponse<List<GetWalletDto>>> AddWallet(AddWalletDto newWallet);
        Task<ServiceResponse<GetWalletDto>> UpdateWallet(UpdateWalletDto updatedWallet);
        Task<ServiceResponse<List<GetWalletDto>>> DeleteWallet(string id);
    }
}
