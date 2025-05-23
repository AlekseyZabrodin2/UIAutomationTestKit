using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using System.Text.Json;

namespace UiAutoTests.TestCasesData
{
    public class TestDataFromJson
    {
        public static TestDataFromJson TestData { get; set; }


        public TestValidRegistration TestValidRegistration { get; set; } = new();
        public TestInvalidRegistration TestInvalidRegistration { get; set; } = new();





        public static TestDataFromJson TestDataInitialize()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory + "TestDataJson/")
                .AddJsonFile("registrationCase.json")
                .Build();

            IServiceCollection services = new ServiceCollection();

            services.Configure<TestDataFromJson>(configuration.GetSection(nameof(TestDataFromJson)));

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            TestData = serviceProvider.GetRequiredService<IOptions<TestDataFromJson>>().Value;

            return TestData;
        }
    }
}
