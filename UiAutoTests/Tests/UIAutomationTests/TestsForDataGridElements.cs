using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;

namespace UiAutoTests.Tests.UIAutomationTests
{
    public class TestsForDataGridElements : InitializeBaseTest
    {
        private ITestClient _testClient;
        private HtmlReportService _reportService = new();
        private ClientConfigurationHelper _clientConfigurationHelper = new();
        private MainWindowController _mainWindowController;



        public TestsForDataGridElements()
        {
            _testClient = new AutomationTestClient(ClientConfigurationHelper.TestClientProperties.TestClientPath);
        }



        [Test]
        public async Task Test01_RegistrationSeveralUsersNextVariant([Values(10)] int number)
        {
            _mainWindowController = await GetControllerState<MainWindowController>(Main);
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .RegistrationSeveralUsers(number)
                        .AssertRowCountIsNot(number);
            });
        }

        [Test]
        public void Test02_RegistrationSeveralUsersNextVariant([Values(10)] int number)
        {
            try
            {
                var mainWindow = _clientConfigurationHelper.StartClientWithCopyRowVirtualizationConfig(_testName, _testClass, _reportService, _testClient);

                _mainWindowController = mainWindow as MainWindowController
                    ?? throw new InvalidCastException("Client state is not MainWindowController.");

                _mainWindowController.ExecuteTest(_testClient, _testName, () =>
                {
                    _mainWindowController
                            .RegistrationSeveralUsers(number)
                            .AssertRowCountIs(number);
                });
            }
            finally
            {
                _clientConfigurationHelper.CopyDefaultConfigFile();
            }            
        }
    }
}
