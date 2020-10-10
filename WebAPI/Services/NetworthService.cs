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
    public class NetworthService
    {
        private CryptoFolioContext _context;
        private readonly IMapper _mapper;

        public NetworthService(CryptoFolioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<Networth[]> GetNetworthByWalletIdAsync(int walletId)
        {
            IQueryable<Networth> query = _context.Networth;
            query = query.Where(n => n.WalletId == walletId);
            return await query.ToArrayAsync();
        }
    }
}
