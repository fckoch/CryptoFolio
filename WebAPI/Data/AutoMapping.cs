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
            CreateMap<User, UserModel>()
            .ReverseMap();

            CreateMap<User, UserRegisterModel>()
            .ReverseMap();

            CreateMap<Wallet, WalletModel>();

            CreateMap<Coin, CoinModel>();

            CreateMap<WalletCoin, WalletCoinModel>()
                .ForMember(c => c.CoinName, options => options.MapFrom(m => m.Coin.CoinName))
                .ForMember(c => c.CoinId, options => options.MapFrom(m => m.Coin.CoinId))
                .ForMember(c => c.CurrentValue, options => options.MapFrom(m => m.Coin.CurrentValue))
                .ReverseMap();
        }
    }
}
