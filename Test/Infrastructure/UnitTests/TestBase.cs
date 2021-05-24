using System;
using Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Test.Infrastructure.UnitTests
{
    public class TestBase
    {
        protected IOptions<DatabaseOptions> databaseOptions;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var config = InitConfiguration();
            var databaseConfig = new DatabaseOptions();
            config.GetSection(DatabaseOptions.Key).Bind(databaseConfig);

            databaseOptions = Options.Create(databaseConfig);
        }

        public IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}