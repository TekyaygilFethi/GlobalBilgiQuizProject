using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.Services.CacheServiceFolder
{
    public interface ICacheService
    {
        T Get<T>(string key);
        bool Set<T>(string key, T value, DateTimeOffset expirationTime);
        bool Set<T>(string key, IEnumerable<T> value, DateTimeOffset expirationTime);
        bool Remove(string key);
        bool Exists(string key);
    }
}