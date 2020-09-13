using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CryptoFolioWorkerService.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CryptoFolioWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                //Create scope and add dbcontext to scope
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CryptoFolioContext>();

                //CryptoQuote API Request
                var httpClient = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();
                var url = "https://api.nomics.com/v1/currencies/ticker?key=8f36e5ca523d2a50571ba779b506fc1e";
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

                List<CoinData> cryptoQuoteCoinList = new List<CoinData>();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    cryptoQuoteCoinList = JsonConvert.DeserializeObject<CoinData[]>(resp).ToList();
                }

                //IQueryable<Coin> query = dbContext.Coin;
                int newCoinCount = 0;
                int updateCoinCount = 0;

                foreach (var coin in cryptoQuoteCoinList)
                {
                    //Checks if coin from current loop already exists in DB
                    if (int.Parse(coin.rank) > 1000)
                        break;

                    var dbCoin = dbContext.Coin.Where(c => c.CoinName == coin.name).FirstOrDefault();

                    try
                    {
                        if (dbCoin != null)
                        {
                            //If coin from loop already exists, updates properties.
                            dbCoin.CurrentValue = Decimal.Parse(coin.price);
                            if (coin.oneD != null)
                            {
                                dbCoin.Price_change = Decimal.Parse(coin.oneD.price_change);
                                dbCoin.Price_change_pct = Decimal.Parse(coin.oneD.price_change_pct);
                            }
                            dbCoin.Rank = int.Parse(coin.rank);
                            if (await dbContext.SaveChangesAsync() > 0)
                                updateCoinCount++;
                                Console.WriteLine($"Coin {coin.name} updated. UpdateCoinCount: {updateCoinCount}");
                        }
                        else
                        {
                            //If coin from loops doesnt exist, add new coin to DB.
                            Coin newCoin = new Coin();
                            newCoin.CoinName = coin.name;
                            if (coin.oneD != null)
                            {
                                newCoin.CurrentValue = Decimal.Parse(coin.price);
                                newCoin.Price_change = Decimal.Parse(coin.oneD.price_change);
                                newCoin.Price_change_pct = Decimal.Parse(coin.oneD.price_change_pct);
                            }
                            newCoin.Rank = int.Parse(coin.rank);
                            newCoin.Symbol = coin.symbol;
                            dbContext.Coin.Add(newCoin);
                            if (await dbContext.SaveChangesAsync() > 0)
                                newCoinCount++;
                                Console.WriteLine($"Coin {coin.name} updated. UpdateCoinCount: {newCoinCount}");
                        }

                    }

                    catch (Exception ex)
                    {

                        //throw;
                        _logger.LogInformation($"Exception error - {ex.ToString()}");
                    }
 
                }

                _logger.LogInformation($"{newCoinCount} new coins were added to DB and {updateCoinCount} were updated");

                await Task.Delay(10000, stoppingToken);

            }
        }
    }
}
