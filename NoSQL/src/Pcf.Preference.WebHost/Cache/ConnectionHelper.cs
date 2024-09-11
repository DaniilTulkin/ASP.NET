using StackExchange.Redis;

namespace Pcf.Preference.WebHost.Cache
{
    public class ConnectionHelper
    {
        
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
        
        static ConnectionHelper()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
                return ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisURL"]);
            });
        }
    }
}
