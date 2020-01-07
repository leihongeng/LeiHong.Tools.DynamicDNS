using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LogLevel = Com.Ctrip.Framework.Apollo.Logging.LogLevel;

namespace LeiHong.Tools.DynamicDNS.Sample.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, builder) =>
                    {
                        LogManager.UseConsoleLogging(LogLevel.Debug);
                        builder
                            .AddApollo("LeiHong.Tools.DynamicDNS", "https://dev-apollo.aiproject.cn/")
                            .AddNamespace("aiproject.TencentKey")
                            .AddNamespace("DynamicDNS");
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
