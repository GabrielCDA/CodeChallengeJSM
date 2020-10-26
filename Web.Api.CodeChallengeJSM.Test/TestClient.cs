using Libary.CodeChallengeJSM.Class;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Structure.CodeChallengeJSM;
using System.Collections.Generic;

namespace Web.Api.CodeChallengeJSM.Test
{
    public class TestClient : WebApplicationFactory<Startup>
    {

       
        private IEnumerable<User> CreatListClients()
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
        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()               
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>()
                        .UseTestServer();
                })
             .ConfigureServices((ctx, service) =>
             {
                 IEnumerable<User> clients = CreatListClients();
                 service.AddSingleton(_ => new Clients(clients));
             });
    }
}
