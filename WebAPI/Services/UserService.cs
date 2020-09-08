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
    public class UserService
    {
        private UserContext _context;
        private readonly IMapper _mapper;

        public UserService(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            IQueryable<User> query = _context.Users;
            query = query.OrderBy(c => c.UserId).Include(c => c.Wallet).ThenInclude(c => c.Walletcoins).ThenInclude(c => c.Coin);

            return await query.ToArrayAsync();
        }

        public async Task<User> GetUserAsync(int Id)
        {
            IQueryable<User> query = _context.Users;
            query = query.Where(c => c.UserId == Id).Include(c => c.Wallet).ThenInclude(c => c.Walletcoins).ThenInclude(c => c.Coin);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<WalletCoin> GetWalletCoinAsync(int WalletId, int WalletCoinId)
        {
            IQueryable<WalletCoin> query = _context.WalletCoins;

            query = query.Where(wc => wc.WalletId == WalletId && wc.WalletCoinId == WalletCoinId).Include(wc => wc.Coin);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<WalletCoin[]> GetWalletCoinsAsync(int WallteId)
        {
            IQueryable<WalletCoin> query = _context.WalletCoins;

            query = query.Where(wc => wc.WalletId == WallteId).Include(wc => wc.Coin);

            return await query.ToArrayAsync();
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
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

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
