using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWallet.Models;
using System.Security.Claims;

namespace MyWallet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        public readonly IWalletServices _walletServices;
       
        public WalletController(IWalletServices walletServices) 
        {
            _walletServices = walletServices;    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetWalletDto>>> Get(string id)
        {
            return Ok(await _walletServices.GetWalletById(id));
        }

        [HttpGet("List")]
        public async Task<ActionResult<ServiceResponse<List<GetWalletDto>>>> GetList()
        {
            return Ok(await _walletServices.GetAllWallets());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetWalletDto>>> PostWallet(AddWalletDto newWallet)
        {
            return Ok(await _walletServices.AddWallet(newWallet));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetWalletDto>>> PutWallet(UpdateWalletDto updatedWallet)
        {
            var response = await _walletServices.UpdateWallet(updatedWallet);
            if (response.data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetWalletDto>>>> DeleteWallet(string id)
        {
            return Ok(await _walletServices.DeleteWallet(id));
        }
    }
}