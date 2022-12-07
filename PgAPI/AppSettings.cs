using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PgAPI
{
    public class AppSettings
    {
        private readonly IConfiguration _config;
        public AppSettings(IConfiguration config)
        {
            _config = config;
            UserPasswordRegex = GetValueOrDefault<string>("User.PasswordRegex", "[0-9]{1+}[A-Z]{1+}");
        }

        public string JwtSecretKey { get; set; }
        public short RefreshTokenTTL { get; set; }
        public string UserPasswordRegex { get; set; }

        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            try
            {
                return (T)Convert.ChangeType(_config[key], typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}