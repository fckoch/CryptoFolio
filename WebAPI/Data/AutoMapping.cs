using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoFolioAPI.Models;
using CryptoFolioAPI.Models.Entities;

namespace CryptoFolioAPI.Data
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserModel>();
            CreateMap<Wallet, Wallet>();
            CreateMap<WalletCoin, WalletCoin>().ForMember(x => x.Coin, option => option.MapFrom(x => x.Coin));
            CreateMap<Coin, Coin>();
        }
    }
}
