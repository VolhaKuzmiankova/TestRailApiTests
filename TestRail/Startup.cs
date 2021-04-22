using Microsoft.Extensions.Configuration;
using TestRail.Settings;

namespace TestRail
{
    public class Startup
    {
        private readonly AppSettings _appSettings = new AppSettings();

        public static AppSettings AppSettings { get; set; }

        public Startup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            configuration.Bind(_appSettings);
            AppSettings = _appSettings;
        }
    }
}