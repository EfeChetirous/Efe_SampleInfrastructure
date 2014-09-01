using System;
using System.Web;

namespace Infrastructure.Core.Data
{
    public enum CacheStatus
    {
        Exists,
        NotExists
    }

    public static class Cache
    {
        public static void Add<T>(string key, T value)
        {
            if (!Exists(key))
                HttpContext.Current.Cache.Insert(
                    key,
                    value,
                    null,
                    DateTime.Now.AddMinutes(1440),
                    System.Web.Caching.Cache.NoSlidingExpiration);
            else
                HttpContext.Current.Cache[key] = value;
        }

        public static void Clear(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public static bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        public static T Get<T>(string key)
        {
            return (T)HttpContext.Current.Cache[key];
        }
    }
}