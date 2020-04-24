using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conductor
{
    public static class EnvironmentVariables
    {
        public static string DbHost => "mongodb+srv://wfuser:logo12Q2@pdf4mecluster-zhvoy.azure.mongodb.net/test?retryWrites=true&w=majority"; // Environment.GetEnvironmentVariable("dbhost");
        public static string Redis => "dmscacheq1.redis.cache.windows.net:6380,password=qKccuJsH6l+vy14KQdvy3DRLPLnLPL2NaTPJsjH3+Gg=,ssl=True,abortConnect=False"; // Environment.GetEnvironmentVariable("redis");
        public static string Auth => Environment.GetEnvironmentVariable("auth");
        public static string PublicKey => Environment.GetEnvironmentVariable("publickey");
        public static string Alg => Environment.GetEnvironmentVariable("alg");
    }
}
