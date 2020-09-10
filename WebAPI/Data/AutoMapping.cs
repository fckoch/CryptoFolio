using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoFolioAPI.Models;
using CryptoFolioAPI.Models.Entities;
using CryptoFolioAPI.Models.ValueObjects;

namespace CryptoFolioAPI.Data
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, OutputUserModel>()
            .ReverseMap();

            CreateMap<User, InputUserModel>()
            .ReverseMap();

            CreateMap<User, InputAuthenticateModel>()
            .ReverseMap();

            CreateMap<TokenObject, OutputAuthenticateModel>()
            .ReverseMap();

            CreateMap<Wallet, InputWalletModel>();

            CreateMap<Wallet, OutputWalletModel>();

            CreateMap<Coin, CoinModel>();

            CreateMap<WalletCoin, InputWalletCoinModel>()
                .ForMember(c => c.CoinName, options => options.MapFrom(m => m.Coin.CoinName))
                .ForMember(c => c.CoinId, options => options.MapFrom(m => m.Coin.CoinId))
                .ForMember(c => c.CurrentValue, options => options.MapFrom(m => m.Coin.CurrentValue))
                .ReverseMap();

            CreateMap<WalletCoin, OutputWalletCoinModel>()
                .ForMember(c => c.CoinName, options => options.MapFrom(m => m.Coin.CoinName))
                .ForMember(c => c.CoinId, options => options.MapFrom(m => m.Coin.CoinId))
                .ForMember(c => c.CurrentValue, options => options.MapFrom(m => m.Coin.CurrentValue))
                .ReverseMap();
            }
    }
}
