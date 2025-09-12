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
                    .Pause(1500);
            });
        }

        [Test]
        public void Test02_ShouldContainMainItems()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                    .AssertMainMenuItemsCountIs(7);
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

        /// Если контекстное меняю может раскрываться то используем метод [Expand]
        /// если больше нет подменю, к последнему используем [Click]
        [TestCase("CoursesMenuItem", "ProgrammingMenuItem", "C#MenuItem")]
        [TestCase("CoursesMenuItem", "DataScienceMenuItem", "MachineLearningMenuItem")]
        [TestCase("TestsMenuItem", "C#TestMenuItem", "")]        
        [Test, Category("Menu"), Category("Parameterized")]
        public void Test04_MenuNavigationTest(string mainItem, string subItem, string subSubItem)
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                _mainWindowController
                    .ExpandMenuItemById(mainItem)
                    .Pause(1500);

                if (!string.IsNullOrEmpty(subItem))
                {
                    _mainWindowController
                        .ExpandMenuItemById(subItem)
                        .Pause(1500);

                    if (!string.IsNullOrEmpty(subSubItem))
                    {
                        _mainWindowController
                            .ClickMenuItemById(subSubItem)
                            .Pause(1500);
                    }
                }

                // Assert something ...
            });
        }

        [Test]
        public void Test05_OpenMessageBoxFromMenuItem()
        {
            _mainWindowController = GetController<MainWindowController>();
            _mainWindowController.ExecuteTest(_testClient, _testName, () =>
            {
                var menuItem = "CoursesMenuItem";
                var subItem = "ProgrammingMenuItem";
                var subSubItem = "C#MenuItem";
                var expectedText = "There must be some important information";

                _mainWindowController
                    .ExpandMenuItemById(menuItem)
                    .ExpandMenuItemById(subItem)
                    .ClickMenuItemById(subSubItem)
                    .Pause(1500)
                    .FindMessageBox()
                    .GetMessageBoxText()
                    .AssertThatElementTextMatches(expectedText);
            });
        }



    }
}
