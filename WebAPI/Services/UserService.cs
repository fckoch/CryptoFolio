using AutoMapper;
using CryptoFolioAPI.Data;
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

        public async Task<T> GetUserAsync2<T>(int Id)
        {
            IQueryable<User> query = _context.Users;
            query = query.Where(c => c.UserId == Id);

            return await _mapper.ProjectTo<T>(query).FirstOrDefaultAsync();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
