using AutoMapper;
using CryptoFolioAPI.Models;
using CryptoFolioAPI.Models.Entities;
using CryptoFolioAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Controllers
{
    [ApiController]
    [Route("api/user/{UserId}/wallet/{WalletId}/walletcoins")]
    public class WalletCoinController : ControllerBase
    {
        private UserService _userService;
        private IMapper _mapper;

        public WalletCoinController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<WalletCoinModel[]>> Get(int WalletId)
        {
            try
            {
                var walletcoins = await _userService.GetWalletCoinsAsync(WalletId);
                return _mapper.Map<WalletCoinModel[]>(walletcoins);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }

        //Get specific walletcoin
        [HttpGet("{id:int}")]
        public async Task<ActionResult<WalletCoinModel>> Get(int WalletId, int id)
        {
            try
            {
                var walletcoin = await _userService.GetWalletCoinAsync(WalletId, id);
                if (walletcoin == null) return NotFound("Wallet not found");
                return _mapper.Map<WalletCoinModel>(walletcoin);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }

        //Post a new walletcoin into a user's wallet
        [HttpPost]
        public async Task<ActionResult<WalletCoinModel>> Post(int UserId, WalletCoinModel model)
        {
            try
            {
                var user = await _userService.GetUserAsync(UserId);
                if (user == null) return BadRequest("User does not exist");

                var walletcoin = _mapper.Map<WalletCoin>(model);
                walletcoin.Coin = await _userService.GetCoinAsync(walletcoin.Coin.CoinId);
                walletcoin.WalletId = user.Wallet.WalletId;

                user.Wallet.Walletcoins.Add(walletcoin);

                if (await _userService.SaveChangesAsync())
                {
                    return Created($"api/user/{user.UserId}/wallet/{user.Wallet.WalletId}/walletcoins/{user.Wallet.Walletcoins.FirstOrDefault().WalletCoinId}", _mapper.Map<WalletCoinModel>(walletcoin));
                }
                else
                {
                    return BadRequest("Failed to save new WalletCoin");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }

    }
}
