using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Models
{
    public class InputWalletModel
    {
        public int WalletId { get; set; }
        public virtual ICollection<InputWalletCoinModel> Walletcoins { get; set; }
    }
}
