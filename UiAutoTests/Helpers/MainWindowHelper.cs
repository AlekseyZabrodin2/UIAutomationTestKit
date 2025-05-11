using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using NLog;
using UiAutoTests.Extensions;
using UiAutoTests.Locators;

namespace UiAutoTests.Helpers
{
    internal class MainWindowHelper
    {

        private readonly Window _window;
        private readonly ConditionFactory _conditionFactory;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();
        private MainWindowLocators _mainWindowLocators;


        public MainWindowHelper(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
            _mainWindowLocators = new(_window, _conditionFactory);
        }


        public void SetUserId()
        {
            _loggerHelper.LogEnteringTheMethod();

            var userIdTextBox = _mainWindowLocators.UserIdTextBox;

            userIdTextBox.FocusTextBoxAndSetCursor();
            userIdTextBox.EnterText("Test ID");

            //userIdTextBox.ClickButton();


            var cleanUpButton = _mainWindowLocators.CleanUpFieldsButton;
            cleanUpButton.ClickButton();

            Thread.Sleep(1500);
        }








    }
}
