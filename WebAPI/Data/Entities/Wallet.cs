using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolio.Data.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public decimal Bitcoin { get; set; }
        public decimal Ethereum { get; set; }
        public decimal Litecoin { get; set; }
    }
}
