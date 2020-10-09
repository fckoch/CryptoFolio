using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioWorkerService.Data
{
    public class CryptoFolioContext : DbContext
    {
        public CryptoFolioContext(DbContextOptions<CryptoFolioContext> options) : base(options)
        {

        }

        public DbSet<Coin> Coin { get; set; }
        public DbSet<WalletCoin> WalletCoins { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Networth> Networth { get; set; }
    }
}
