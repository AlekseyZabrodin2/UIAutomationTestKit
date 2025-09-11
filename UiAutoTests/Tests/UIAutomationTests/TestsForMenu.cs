using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;

namespace UiAutoTests.Tests.UIAutomationTests
{
    public class TestsForMenu : InitializeBaseTest
    {

        private ITestClient _testClient;
        private MainWindowController _mainWindowController;


        public TestsForMenu()
        {
            _testClient = new AutomationTestClient(ClientConfigurationHelper.TestClientProperties.TestClientPath);
        }

        

        [Test]
        public void Test01_CombiningIntoOneMethod()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .SetValidDataInUserForm(1, 3)
                        .AssertIsRegistrationButtonEnabled()
                        .ClickRegistrationButton()
                        .WaitUntilProgressBarIs(3);
            });
        }

    }
}
