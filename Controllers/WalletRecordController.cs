using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallet.DTOs.WalletRecord;
using MyWallet.Models;
using MyWallet.Services.WalletRecordServices;

namespace MyWallet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WalletRecordController : ControllerBase 
    {
        private readonly IWalletRecordServices _walletRecordServices;

        public WalletRecordController(IWalletRecordServices walletRecordServices)
        {
            _walletRecordServices = walletRecordServices;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetWalletDto>>> AddWalletRecord(AddWalletRecordDto newRecordWallet)
        {
            return Ok(await _walletRecordServices.AddWalletRecord(newRecordWallet));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetWalletRecordDto>>>> GetAllWalletRecords()
        {
            return Ok(await _walletRecordServices.GetAllWalletRecords());
        }
    }
}
