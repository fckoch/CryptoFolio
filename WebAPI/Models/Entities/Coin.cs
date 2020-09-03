using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolio.Models.Entities
{
    public class Coin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentValue { get; set; }
    }
}
