
using MyWallet.Models;
using System.Security.Claims;

namespace MyWallet.Services.WalletServices
{
    public class WalletServices : IWalletServices
    {     

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WalletServices(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private Guid GetUserId() => new Guid(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public async Task<ServiceResponse<List<GetWalletDto>>> AddWallet(AddWalletDto newWallet)
        {
            var serviceResponse = new ServiceResponse<List<GetWalletDto>>();
            
            Wallet wallet = _mapper.Map<Wallet>(newWallet);
            wallet.user = await _context.Users.FirstOrDefaultAsync(u => u.id == GetUserId());

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            
            serviceResponse.data = await _context.Wallets
                .Where(w => w.user!.id == GetUserId())
                .Select(w => _mapper.Map<GetWalletDto>(w))
                .ToListAsync();
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetWalletDto>>> GetAllWallets()
        {
            var serviceResponse = new ServiceResponse<List<GetWalletDto>>();
            var dbWallets = await _context.Wallets.Where(w => w.user!.id == GetUserId()).ToListAsync();
            serviceResponse.data = dbWallets.Select(w => _mapper.Map<GetWalletDto>(w)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetWalletDto>> GetWalletById(string id)
        {
            var serviceResponse = new ServiceResponse<GetWalletDto>();
            var dbWallets = await _context.Wallets
                .FirstOrDefaultAsync(w => w.id == new Guid(id) && w.user!.id == GetUserId());
            serviceResponse.data = _mapper.Map<GetWalletDto>(dbWallets);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetWalletDto>> UpdateWallet(UpdateWalletDto updatedWallet)
        {
            var serviceResponse = new ServiceResponse<GetWalletDto>();

            try
            {
                var wallet = await _context.Wallets
                    .Include(w => w.user)
                    .FirstOrDefaultAsync(w => w.id == updatedWallet.id);
                if (wallet is null || wallet.user!.id == GetUserId())
                    throw new Exception($"Wallet with id: {updatedWallet.id} not found!");

                wallet.name = updatedWallet.name;
                wallet.currency = updatedWallet.currency;
                wallet.description = updatedWallet.description;
                
                await _context.SaveChangesAsync();

                serviceResponse.data = _mapper.Map<GetWalletDto>(wallet);
            
            }catch(Exception e)
            {
                serviceResponse.success = false;
                serviceResponse.message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetWalletDto>>> DeleteWallet(string id)
        {
            var serviceResponse = new ServiceResponse<List<GetWalletDto>>();

            try
            {
                var wallet = await _context.Wallets.FirstAsync(w => w.id == new Guid(id) && w.user!.id == GetUserId());
                if (wallet is null)
                    throw new Exception($"Wallet with id: {id} not found!");

                _context.Wallets.Remove(wallet);
                await _context.SaveChangesAsync();
                                
                serviceResponse.data = await _context.Wallets
                    .Where(w => w.user!.id == GetUserId())
                    .Select(w => _mapper.Map<GetWalletDto>(w)).ToListAsync();

            }
            catch (Exception e)
            {
                serviceResponse.success = false;
                serviceResponse.message = e.Message;
            }

            return serviceResponse;
        }
    }
}
