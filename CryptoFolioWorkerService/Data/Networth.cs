using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioWorkerService
{
    public class Networth
    {
        public int NetworthId { get; set; }
        public int WalletId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal NetworthValue { get; set; }
    }
}
