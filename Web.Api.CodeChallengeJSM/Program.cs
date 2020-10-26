using Libary.CodeChallengeJSM.Class;
using Libary.CodeChallengeJSM.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Structure.CodeChallengeJSM;
using System.Collections.Generic;

namespace Web.Api.CodeChallengeJSM
{
    public class Program
    {
        private static IEnumerable<User> clients;
        public static void Main(string[] args)
        {
            clients = CreatListClients();            
            CreateHostBuilder(args).Build().Run();
        }
        private static IEnumerable<User> CreatListClients()
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json")
                      .Build();
                string urlJson = configuration.GetSection("URLs").Get<Dictionary<string, string>>()["urlJson"];
                string urlCSV = configuration.GetSection("URLs").Get<Dictionary<string, string>>()["urlCSV"]; 

                var loadAllClients = RequestClients.RequestStructureJsonAndCSV(urlJson, urlCSV);
                return loadAllClients;
            }
            catch
            {
                return null;
            }
            
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices((context, service) =>
            {
                service.AddSingleton(_ => new Clients(clients));
            });


    }
}
