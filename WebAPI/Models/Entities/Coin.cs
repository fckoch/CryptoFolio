using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolio.Models.Entities
{
    public class Coin
    {
        public string Name { get; set; }
        public decimal ValueWhenBought { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
