using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace eReconciliationProject.Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));        
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return Get<object>(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); // boş gelebilme durumu için _ ekledik
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {


            var cacheExtriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var cacheEntriesCollection = cacheExtriesCollectionDefinition.GetValue(_memoryCache) as dynamic;

            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var item in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = item.GetType().GetProperty("Value").GetValue(item,null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var keyRemove = cacheCollectionValues.Where(x=>regex.IsMatch(x.Key.ToString())).Select(x=>x.Key).ToList();

            foreach (var item in keyRemove)
            {
                _memoryCache.Remove(item);
            }
        }
    }
}
