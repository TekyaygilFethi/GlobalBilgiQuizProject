﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.Services.RedisServiceFolder
{
    public interface IRedisService
    {
        void Connect();
        IDatabase GetDb(int db = 1);
    }
}
