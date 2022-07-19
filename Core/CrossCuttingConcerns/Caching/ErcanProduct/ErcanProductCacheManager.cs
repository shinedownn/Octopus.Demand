 
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.ErcanProduct
{
    public class ErcanProductCacheManager : ICacheManager
    {
        private readonly RedisEndpoint _redisEndpoint; 
        public ErcanProductCacheManager() 
        { 
            var section= ServiceTool.ServiceProvider.GetService<IConfiguration>().GetSection("ErcanProduct");
            var redisIp = section.GetValue<string>("RedisIp");
            var redisPort = section.GetValue<int>("Port");
            _redisEndpoint = new RedisEndpoint(redisIp, redisPort);
        }

        public T Get<T>(string key)
        {
            var result = default(T);
            RedisInvoker(x => { result = x.Get<T>(key); });
            return result;
        }

        public object Get(string key)
        {
            var result = default(object);
            RedisInvoker(x => { result = x.Get<object>(key); });
            return result;
        }

        public void Add(string key, object data, int duration)
        {
            RedisInvoker(x => x.Add(key, data, TimeSpan.FromMinutes(duration)));
        }

        public void Add(string key, object data)
        {
            RedisInvoker(x => x.Add(key, data));
        }

        public bool IsAdd(string key)
        {
            var isAdded = false;
            RedisInvoker(x => isAdded = x.ContainsKey(key));
            return isAdded;
        }

        public void Remove(string key)
        {
            RedisInvoker(x => x.Remove(key));
        }

        public void RemoveByPattern(string pattern)
        {
            RedisInvoker(x => x.RemoveByPattern(pattern));
        }

        public void Clear()
        {
            RedisInvoker(x => x.FlushAll());
        }

        private void RedisInvoker(Action<RedisClient> redisAction)
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                redisAction.Invoke(client);
            }
        }
    }
}
