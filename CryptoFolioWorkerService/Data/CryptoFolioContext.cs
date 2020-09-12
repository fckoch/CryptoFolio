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
    }
}
