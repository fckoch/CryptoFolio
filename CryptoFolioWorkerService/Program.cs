using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CryptoFolioWorkerService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoFolioWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration(config => config.AddUserSecrets(Assembly.GetExecutingAssembly()))
                .ConfigureServices((hostContext, services) =>
                {
                    //var cryptoQuoteApiKey = hostContext.Configuration.GetSection("CryptoQuote:ApiKey").Value;

                    //if (string.IsNullOrWhiteSpace(cryptoQuoteApiKey))
                    //    throw new InvalidOperationException("Can not start the service without a valid CryptoQuoteApiKey API key!");

                    services.AddDbContext<CryptoFolioContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("CryptoFolio")));
                    services.AddHostedService<Worker>();
                    services.AddHttpClient();
                });

            return host;
        }
    }
}
