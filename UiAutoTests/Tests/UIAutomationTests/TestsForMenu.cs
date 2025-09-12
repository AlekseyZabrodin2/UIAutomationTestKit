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
        public void Test01_CanNavigateToCSharpCourse()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                    .ExpandMenuItemById("CoursesMenuItem")
                    .ExpandMenuItemById("ProgrammingMenuItem")
                    .ClickMenuItemById("C#MenuItem")
                    .Pause(3000);
            });
        }

        [Test]
        public void Test02_ShouldContainMainItems()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                    .AssertMainMenuItemsCountIs(7)
                    .Pause(3000);
            });
        }

        [Test]
        public void Test03_ShouldContainSubitemsCount()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                var menuItem = "CoursesMenuItem";
                var subItem = "DataScienceMenuItem";

                _mainWindowController
                    .ExpandMenuItemById(menuItem)
                    .ExpandMenuItemById(subItem)
                    .Pause(1500)
                    .AssertSubItemsCountIs(subItem, 3);
            });
        }







    }
}
