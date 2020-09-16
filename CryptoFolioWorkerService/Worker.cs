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

                int addCount = 0;
                int updateCount = 0;

                foreach (var coin in cryptoQuoteCoinList)
                {

                    //Will only loop until specified rank
                    if (int.Parse(coin.rank) > 1320)
                        break;

                    //Checks if coin from current loop already exists in DB
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
                            updateCount++;

                            //Save changes to db each 100 new coins updated
                            if (updateCount == 100)
                            {
                                if (await dbContext.SaveChangesAsync() > 0)
                                    Console.WriteLine($"{updateCount} coins were updated");
                                    updateCount = 0;
                            }

                        }
                        else
                        {
                            //If coin from loop doesnt exist, add new coin to DB.
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
                            addCount++;

                            //Save changes to db each 100 new coins added
                            if (addCount == 100)
                            {
                                if (await dbContext.SaveChangesAsync() > 0)
                                    Console.WriteLine($"{addCount} coins were added");
                                    addCount = 0;
                            }

                        }

                    }

                    catch (Exception ex)
                    {

                        //throw;
                        _logger.LogInformation($"Exception error - {ex.ToString()}");
                    }

                }

                //Save changes to db for remaining coins
                if (await dbContext.SaveChangesAsync() > 0)
                    Console.WriteLine($"{addCount} coins were added");
                    Console.WriteLine($"{updateCount} coins were updated");

                await Task.Delay(10000, stoppingToken);

            }
        }
    }
}
