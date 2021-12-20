using System;
using System.Threading.Tasks;

namespace SmartLockDemo.Data.Utilities
{
    /// <summary>
    /// Provides some functionalities to communicate with Redis distributed cache
    /// </summary>
    internal interface IRedisClient
    {
        /// <summary>
        /// Receives value from Redis by given key
        /// </summary>
        /// <param name="key">Key of value to receive</param>
        /// <returns></returns>
        string Get(string key);
        Task<string> GetAsync(string key);
        /// <summary>
        /// Sets a value in Redis by given key and value pair
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        void Set(string key, string value);
        Task SetAsync(string key, string value);
    }
}
