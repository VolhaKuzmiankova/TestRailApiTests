using Microsoft.Extensions.Configuration;

namespace TestRail.Settings
{
    public static class AppSettings
    {
        public static readonly IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
}