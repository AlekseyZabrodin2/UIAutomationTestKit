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
        public void Test01_Can_Navigate_To_CSharp_Course()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                        .ExpandMenuItemById("CoursesMenuItem")
                        .ExpandMenuItemById("ProgrammingMenuItem")
                        .ClickMenuItemById("C#MenuItem");

                Thread.Sleep(3000);
            });
        }

    }
}
