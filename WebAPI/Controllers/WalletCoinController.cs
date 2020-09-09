using AutoMapper;
using CryptoFolioAPI.Models;
using CryptoFolioAPI.Models.Entities;
using CryptoFolioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/wallets/{WalletId}/walletcoins")]
    public class WalletCoinController : ControllerBase
    {
        private readonly WalletService _walletService;
        private readonly WalletCoinService _walletCoinService;
        private readonly CoinService _coinService;
        private readonly IMapper _mapper;

        public WalletCoinController(WalletService walletService, WalletCoinService walletCoinService, CoinService coinService, IMapper mapper)
        {
            _walletService = walletService;
            _walletCoinService = walletCoinService;
            _coinService = coinService; 
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OutputWalletCoinModel[]>> Get(int WalletId)
        {
            try
            {
                var walletcoins = await _walletCoinService.GetWalletCoinsAsync(WalletId);
                return _mapper.Map<OutputWalletCoinModel[]>(walletcoins);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }

        //Get specific walletcoin
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OutputWalletCoinModel>> Get(int WalletId, int id)
        {
            try
            {
                var walletcoin = await _walletCoinService.GetWalletCoinAsync(WalletId, id);
                if (walletcoin == null) return NotFound("Wallet not found");
                return _mapper.Map<OutputWalletCoinModel>(walletcoin);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }

        //Post a new walletcoin into a user's wallet
        [HttpPost]
        public async Task<ActionResult<OutputWalletCoinModel>> Post(int WalletId, InputWalletCoinModel model)
        {
            try
            {
                var wallet = await _walletService.GetWalletAsync(WalletId);
                if (wallet == null) return BadRequest("Wallet does not exist");

                var walletcoin = _mapper.Map<WalletCoin>(model);
                walletcoin.Coin = await _coinService.GetCoinAsync(walletcoin.Coin.CoinId);
                walletcoin.WalletId = wallet.WalletId;

                wallet.Walletcoins.Add(walletcoin);

                if (await _walletCoinService.SaveChangesAsync())
                {
                    return Created($"api/wallets/{wallet.WalletId}/walletcoins/{wallet.Walletcoins.FirstOrDefault().WalletCoinId}", _mapper.Map<OutputWalletCoinModel>(walletcoin));
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
