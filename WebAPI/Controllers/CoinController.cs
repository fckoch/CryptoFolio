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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CoinModel>> Get (int id)
        {
            try
            {
                var coin = await _coinService.GetCoinAsync(id);
                return _mapper.Map<CoinModel>(coin);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }
    }
}
