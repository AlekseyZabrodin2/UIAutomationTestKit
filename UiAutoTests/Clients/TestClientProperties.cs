using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace UiAutoTests.Clients
{
    public class TestClientProperties
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();


        public string TestClientPath { get; set; }
        public string TestClientDir { get; set; }
        public bool EnableRowVirtualization { get; set; }


        public string ChooseConfig(bool configWithTrue)
        {
            return configWithTrue
                ? Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Clients") 
                : Path.Combine(AppContext.BaseDirectory, "Clients");
        }

        public void LoadConfig(string path)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("testClientProperties.json")
                .Build();

            var section = configuration.GetSection(nameof(TestClientProperties));
            var result = new TestClientProperties();
            section.Bind(result);

            _logger.Info($"Enable Row Virtualization - [{result.EnableRowVirtualization}]");
        }
    }
}
