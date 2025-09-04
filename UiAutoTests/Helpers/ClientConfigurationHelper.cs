using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLog;
using System.Text.Json;
using UiAutoTests.Clients;
using UiAutoTests.Core;
using UiAutoTests.Services;
using UiAutoTests.Tests;

namespace UiAutoTests.Helpers
{
    public class ClientConfigurationHelper
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();

        public static TestClientProperties TestClientProperties { get; private set; }
        private readonly string _configFileName = "testClientProperties.json";

        /// <summary>
        /// If you have some configuration files, and you need load them and change properties
        /// </summary> 



        public void InitializeAppConfig()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory + "Clients/")
                .AddJsonFile("testClientProperties.json")
                .Build();

            IServiceCollection services = new ServiceCollection();

            services.Configure<TestClientProperties>(configuration.GetSection(nameof(TestClientProperties)));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            TestClientProperties = serviceProvider.GetRequiredService<IOptions<TestClientProperties>>().Value;
        }

        public IClientState StartClientWithCopyRowVirtualizationConfig(string testName, string testClass,
            HtmlReportService reportCore, ITestClient testClient)
        {
            _loggerHelper.LogEnteringTheMethod();

            TestsInitializeService initializeService = new();

            SaveFileWithEnableRowVirtualizationFalse(TestClientProperties);
            CopyAppConfigFile(false);

            var mainWindow = initializeService.StartTestClient(testClient).GetAwaiter().GetResult();
            initializeService.InitializeReportingTests(testName, testClass, reportCore);

            return mainWindow;
        }

        public void SaveFileWithEnableRowVirtualizationFalse(TestClientProperties properties)
        {
            TestClientProperties.EnableRowVirtualization = false;

            var json = JsonSerializer.Serialize(
                new { TestClientProperties = properties },
                new JsonSerializerOptions { WriteIndented = true });

            var path = Path.Combine(AppContext.BaseDirectory, "Clients", _configFileName);
            File.WriteAllText(path, json);
        }

        public void CopyAppConfigFile(bool configWithTrue)
        {
            _loggerHelper.LogEnteringTheMethod();

            var clientProperties = new TestClientProperties();

            var filePath = clientProperties.ChooseConfig(configWithTrue);
            var testConfigPath = Path.Combine(filePath, _configFileName);
            var destConfigPath = Path.Combine(TestClientProperties.TestClientDir, _configFileName);

            if (!File.Exists(testConfigPath))
            {
                throw new FileNotFoundException($"The file '{testConfigPath}' does not exist.");
            }

            File.Copy(testConfigPath, destConfigPath, true);
            clientProperties.LoadConfig(filePath);
            _logger.Trace("Config File successfully copied");
        }

        public void CopyDefaultConfigFile()
        {
            _loggerHelper.LogEnteringTheMethod();

            var clientProperties = new TestClientProperties();

            var defaultPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Clients");
            var defaultConfigPath = Path.Combine(defaultPath, _configFileName);
            
            var destinationPathOne = Path.Combine(TestClientProperties.TestClientDir, _configFileName);
            var destinationPathTwo = Path.Combine(AppContext.BaseDirectory, "Clients", _configFileName);

            clientProperties.LoadConfig(defaultPath);

            if (!File.Exists(defaultConfigPath))
            {
                throw new FileNotFoundException($"The file '{defaultConfigPath}' does not exist.");
            }

            File.Copy(defaultConfigPath, destinationPathOne, true);
            File.Copy(defaultConfigPath, destinationPathTwo, true);
            
            _logger.Trace("Config File successfully copied");
        }
    }
}
