using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;

namespace DynamicRefreshExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var settings = config.Build();

                    config.AddAzureAppConfiguration(options =>
                    {
                        options
                            .Connect(settings["ConnectionStrings:AppConfiguration"])
                            .ConfigureRefresh(refreshOptions =>
                            {
                                refreshOptions
                                    .Register(key: "AppSettings:Version", label: LabelFilter.Null, refreshAll: true)
                                    .SetCacheExpiration(TimeSpan.FromSeconds(10));
                            });
                        
                    });
                })
                .UseStartup<Startup>();
    }
}
