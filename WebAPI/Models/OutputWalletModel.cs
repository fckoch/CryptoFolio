using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Models
{
    public class OutputWalletModel
    {
        public int WalletId { get; set; }
        public virtual ICollection<OutputWalletCoinModel> Walletcoins { get; set; }
    }
}
