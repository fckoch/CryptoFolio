﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoFolioAPI.Models
{
    public class OutputNetworthModel
    {
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal NetworthValue { get; set; }
    }
}
