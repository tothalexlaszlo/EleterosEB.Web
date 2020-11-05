using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EleterosEB.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // only for development | validate service scopes
                    webBuilder.UseDefaultServiceProvider(opt =>
                    {
                        opt.ValidateScopes = true;
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
