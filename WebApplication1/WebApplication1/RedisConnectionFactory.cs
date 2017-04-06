using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class RedisConnectionFactory
    {
        private static readonly Lazy<ConnectionMultiplexer> Connection;

        private static readonly string REDIS_CONNECTIONSTRING = "REDIS_CONNECTIONSTRING";

        static RedisConnectionFactory()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config[REDIS_CONNECTIONSTRING];
            //var connectionString = "localhost";

            if (connectionString == null)
            {
                throw new KeyNotFoundException($"Environment variable for {REDIS_CONNECTIONSTRING} was not found.");
            }

            var options = ConfigurationOptions.Parse(connectionString);

            Connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
        }

        public static ConnectionMultiplexer GetConnection() => Connection.Value;
    }
}
