using AspNetCoreApp.Models;
using AspNetCoreApp.Models.Repositories;
using AspNetCoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreApp.Controllers
{
    public class CacheEntryConstants
    {
        public const string PiesOfTheWeek = "PieOfTheWeek";
    }

    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        public HomeController(IPieRepository pieRepository, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _pieRepository = pieRepository;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        public ViewResult Index()
        {
            IEnumerable<Pie> piesOftheWeek = null;// _pieRepository.PiesOftheWeek;
            //if (!_memoryCache.TryGetValue(CacheEntryConstants.PiesOfTheWeek, out piesOftheWeek))
            //{
            //    piesOftheWeek = _pieRepository.PiesOftheWeek;

            //    var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
            //    cacheOptions.RegisterPostEvictionCallback(FillCacheAgain, this);

            //    _memoryCache.Set(CacheEntryConstants.PiesOfTheWeek, piesOftheWeek, cacheOptions);
            //}

            //piesOfTheWeekCached = _memoryCache.GetOrCreate(CacheEntryConstants.PiesOfTheWeek, entry =>
            //{
            //    entry.SlidingExpiration = TimeSpan.FromSeconds(30);
            //    entry.Priority = CacheItemPriority.High;
            //    return _pieRepository.PiesOfTheWeek;
            //});

            piesOftheWeek = GetOrSavePiesFromCache();

            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = piesOftheWeek
            };

            return View(homeViewModel);
        }

        private IEnumerable<Pie> GetOrSavePiesFromCache()
        {
            var cachedPieString = _distributedCache.Get(CacheEntryConstants.PiesOfTheWeek);
            if (cachedPieString == null)
            {
                var pies = _pieRepository.PiesOftheWeek;
                string serializedPies = JsonConvert.SerializeObject(pies);
                byte[] encodedPies = Encoding.UTF8.GetBytes(serializedPies);
                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(1000));
                _distributedCache.Set(CacheEntryConstants.PiesOfTheWeek, encodedPies, cacheOptions);
                return pies;
            }
            else
            {
                byte[] encodedPies = _distributedCache.Get(CacheEntryConstants.PiesOfTheWeek);
                string pieString = Encoding.UTF8.GetString(encodedPies);
                return JsonConvert.DeserializeObject<IEnumerable<Pie>>(pieString);
            }
        }

        private void FillCacheAgain(object key, object value, EvictionReason reason, object state)
        {
            // Callback for cache eviction.
        }
    }
}
