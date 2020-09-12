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
                using var scope = _serviceScopeFactory.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<CryptoFolioContext>();

                var coin = dbContext.Coin.ToArray();
                _logger.LogInformation($"{coin[0].CoinName} current value: {coin[0].CurrentValue}");


                var httpClient = HttpClientFactory.Create();

                var url = "https://api.nomics.com/v1/currencies/ticker?key=8f36e5ca523d2a50571ba779b506fc1e&ids=BTC,ETH,XRP&interval=";
                //var data = await httpClient.GetFromJsonAsync<RootObject[]>(url);
                //var data = JsonSerializer.Deserialize<CoinData>();


                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
                var resp = await httpResponseMessage.Content.ReadAsStringAsync();
                //var data = await httpClient.GetStringAsync(url);

                CoinData[] myDeserializedClass = JsonConvert.DeserializeObject<CoinData[]>(resp);

                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}
