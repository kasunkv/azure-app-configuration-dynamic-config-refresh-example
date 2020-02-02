using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Azure.Identity;

namespace MusicStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) => {
                    var settings = config.Build();
                    var appConfigEndpoint = settings["AppSettings:AppConfiguration:Endpoint"];
                    var userAssignedIdentityClientId = settings["AppSettings:Identity:ClientId"];

                    if (!string.IsNullOrEmpty(appConfigEndpoint))
                    {
                        var endpoint = new Uri(appConfigEndpoint);

                        config.AddAzureAppConfiguration(options =>
                        {
                            options
                                .Connect(endpoint, new ManagedIdentityCredential(clientId: userAssignedIdentityClientId))
                                .UseFeatureFlags();
                        });
                    }

                });
    }
}
