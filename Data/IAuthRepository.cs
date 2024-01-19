using MyWallet.Models;

namespace MyWallet.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<Guid>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string userName, string password);
        Task<bool> UserExsist(string username);

    }
}
