using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyWallet.DTOs.WalletRecord;
using MyWallet.Models;
using MyWallet.Services.WalletRecordServices;
using System.Security.Claims;

namespace MyWallet.Services.TransactionServices
{
    public class WalletRecordServices : IWalletRecordServices
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public WalletRecordServices(DataContext dbContext, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        private Guid GetUserId() => new Guid(_httpContext.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public async Task<ServiceResponse<GetWalletRecordDto>> AddWalletRecord(AddWalletRecordDto newWalletRecord)
        {
            var response = new ServiceResponse<GetWalletRecordDto>();
            try
            {
                var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(w => w.id == newWalletRecord.walletId 
                    && w.user!.id == GetUserId());
                if (wallet is null) 
                {
                    response.success = true;
                    response.message = "Wallet not found!";
                    return response;
                }
                
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.id == GetUserId());
                if (user is null)
                {
                    response.success = true;
                    response.message = "User not found!";
                    return response;
                }

                var walletRecord = _mapper.Map<WalletRecord>(newWalletRecord);
                walletRecord.wallet = wallet;
                walletRecord.user = user;

                _dbContext.WalletRecords.Add(walletRecord);
                await _dbContext.SaveChangesAsync();

                response.data = _mapper.Map<GetWalletRecordDto>(walletRecord);

            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetWalletRecordDto>>> GetAllWalletRecords()
        {
            var response = new ServiceResponse<List<GetWalletRecordDto>>();

            response.data = await _dbContext.WalletRecords
                .Where(w => w.user!.id == GetUserId())
                .Include(w => w.wallet)
                .Select(w => _mapper.Map<GetWalletRecordDto>(w))
                .ToListAsync();
                         
            return response; 
        }
    }
}
