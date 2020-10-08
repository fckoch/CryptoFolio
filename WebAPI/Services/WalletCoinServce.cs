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
    public class WalletCoinService
    {
        private CryptoFolioContext _context;
        private readonly IMapper _mapper;

        public WalletCoinService(CryptoFolioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WalletCoin> GetWalletCoinAsync(int WalletId, int WalletCoinId)
        {
            IQueryable<WalletCoin> query = _context.WalletCoins;

            query = query.Where(wc => wc.WalletId == WalletId && wc.WalletCoinId == WalletCoinId).Include(wc => wc.Coin);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<WalletCoin[]> GetWalletCoinsAsync(int WalletId)
        {
            IQueryable<WalletCoin> query = _context.WalletCoins;

            query = query.Where(wc => wc.WalletId == WalletId).Include(wc => wc.Coin);

            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only returns success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Delete(WalletCoin entity)
        {
            _context.WalletCoins.Remove(entity);
        }
    }
}
