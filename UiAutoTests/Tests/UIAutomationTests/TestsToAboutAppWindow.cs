using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;

namespace UiAutoTests.Tests.UIAutomationTests
{
    public class TestsToAboutAppWindow : InitializeBaseTest
    {
        private ITestClient _testClient;
        private AboutAppWindowController _appWindowController;


        public TestsToAboutAppWindow()
        {
            _testClient = new AutomationTestClient(ClientConfigurationHelper.TestClientProperties.TestClientPath);
        }



        [Test]
        public async Task Test01_FindAboutAppWindow()
        {
            _appWindowController = await GetControllerState<AboutAppWindowController>(AboutApp);
            _appWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _appWindowController
                    .AssertThatAppWindowIsNotNull()
                    .Pause(3000)
                    .CloseAboutAppWindow()
                    .Pause(3000)
                    .AssertThatAppWindowIsNull();
            });
        }










    }
}
