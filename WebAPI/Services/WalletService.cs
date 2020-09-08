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
    public class WalletService
    {
        private readonly CryptoFolioContext _context;
        private readonly IMapper _mapper;

        public WalletService(CryptoFolioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Wallet> GetWalletAsync(int Id)
        {
            IQueryable<Wallet> query = _context.Wallets;
            query = query.Where(w => w.WalletId == Id).Include(w => w.Walletcoins);
            return await query.FirstOrDefaultAsync();
        }
    }
}
