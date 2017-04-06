using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using StackExchange.Redis;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //var dnsTask = Dns.GetHostAddressesAsync("redis");
            //var addresses = dnsTask.Result;
            //var connect = string.Join(",", addresses.Select(x => x.MapToIPv4().ToString() + ":" + "6793"));

            //TODO: Need to make this more robust. Also want to understand why the static connection method cannot accept dns names.
            //var ips = Dns.GetHostAddressesAsync("redis:6379");
            //ips.Wait();
            //var redis = ConnectionMultiplexer.ConnectAsync(,ips.Result.First().ToString());
            //redis.Wait();
            //var db = redis.Result.GetDatabase();
            //services.AddSingleton(db);
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.InstanceName = "BookStore";
            //    options.Configuration = "127.0.0.1:6379";//Environment.GetEnvironmentVariable("REDIS_URL");
            //});

            //Dns.

            services.AddSingleton(new RedisDataAgent());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
