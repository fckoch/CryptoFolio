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
    [Route("api/coins")]
    public class CoinController : ControllerBase
    {
        private readonly CoinService _coinService;
        private readonly IMapper _mapper;

        public CoinController(CoinService coinService, IMapper mapper)
        {
            _coinService = coinService;
            _mapper = mapper;
        }

        //Get all coins
        [HttpGet]
        public async Task<ActionResult<CoinModel[]>> Get()
        {
            try
            {
                var coins = await _coinService.GetAllCoinsAsync();
                return _mapper.Map<CoinModel[]>(coins);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }

        //Get coin by ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CoinModel>> Get (int id)
        {
            try
            {
                var coin = await _coinService.GetCoinAsync(id);
                if (coin == null)
                    return NotFound("Coin not found");
                return _mapper.Map<CoinModel>(coin);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }

        //Update coin value
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CoinModel>> Put(int id, CoinModel model)
        {
            try
            {
                var coin = await _coinService.GetCoinAsync(id);
                if (coin == null)
                    return NotFound("Coin not found");

                _mapper.Map(model, coin);

                if (await _coinService.SaveChangesAsync())
                {
                    return _mapper.Map<CoinModel>(coin);
                }
                else
                {
                    return BadRequest("Failed to update coin value");
                }
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }
    }
}
