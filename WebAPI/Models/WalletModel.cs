using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Models
{
    public class WalletModel
    {
        public int WalletId { get; set; }
        public virtual ICollection<WalletCoinModel> Walletcoins { get; set; }
    }
}
