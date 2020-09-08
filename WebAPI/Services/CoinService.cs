using AutoMapper;
using CryptoFolioAPI.Data;
using CryptoFolioAPI.Models;
using CryptoFolioAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Services
{
    public class CoinService
    {
        private CryptoFolioContext _context;
        private readonly IMapper _mapper;

        public CoinService(CryptoFolioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Coin[]> GetAllCoinsAsync()
        {
            IQueryable<Coin> query = _context.Coin;
            return await query.ToArrayAsync();
        }

        public async Task<Coin> GetCoinAsync(int Id)
        {
            IQueryable<Coin> query = _context.Coin;
            query = query.Where(c => c.CoinId == Id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only returns success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
