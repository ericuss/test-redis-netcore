using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache cache;
        private readonly IDatabase redis;
        private readonly RedisDataAgent redisAgent;

        public HomeController(RedisDataAgent redisAgent)//IDistributedCache cache) // IDatabase redis)
        {
            //this.cache = cache;
            //this.redis = redis;
            this.redisAgent = redisAgent;
        }
        public async Task<IActionResult> Index()
        {
            //var result = await this.redis.StringGetAsync("this_is_a_key");
            //var result = await this.cache.GetStringAsync("this_is_a_key");



            var result = this.redisAgent.GetStringValue("this_is_a_key");
            if (result == null)
            {
                var rand = new Random();
                result = rand.Next(int.MinValue, int.MaxValue).ToString();

                this.redisAgent.SetStringValue("this_is_a_key", result);
                //await this.cache.SetStringAsync("this_is_a_key", result);
                //await this.redis.StringSetAsync("this_is_a_key", result);
            }
            ViewData["redisText"] = result;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
