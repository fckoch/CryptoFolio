using AutoMapper;
using CryptoFolioAPI.Models;
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
    [Route("api/networth")]
    public class NetworthController : ControllerBase
    {
        private readonly NetworthService _networthService;
        private readonly IMapper _mapper;

        public NetworthController(NetworthService networthService, IMapper mapper)
        {
            _networthService = networthService;
            _mapper = mapper;
        }

        //Get networth history by walletId
        [HttpGet("{walletid:int}")]
        public async Task<ActionResult<OutputNetworthModel[]>> Get (int walletid)
        {
            try
            {
                var networths = await _networthService.GetNetworthByWalletIdAsync(walletid);
                return _mapper.Map<OutputNetworthModel[]>(networths);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }
    }
}
