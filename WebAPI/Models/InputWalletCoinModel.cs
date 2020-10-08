using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Models
{
    public class InputWalletCoinModel
    {
        public int WalletCoinId { get; set; }
        public int CoinId { get; set; }
        public string CoinName { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal ValueWhenBought { get; set; }
        public decimal CurrentValue { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
    }
}
