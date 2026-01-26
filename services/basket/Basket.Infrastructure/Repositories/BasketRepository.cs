using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;
        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<ShoppingCart?> GetBasketAsync(string userName)
        {
            var basket = await _distributedCache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        public async Task DeleteBasketAsync(string userName)
        {
            await _distributedCache.RemoveAsync($"basket:{userName}");
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart cart)
        {
            var key = $"basket:{cart.UserName}";
            var basketJson = JsonConvert.SerializeObject(cart);

            await _distributedCache.SetStringAsync(
                key,
                basketJson,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                });

            // رجّع أحدث نسخة (اختياري)
            return (await GetBasketAsync(cart.UserName)) ?? cart;
        }

    }
}
