using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using NLog;
using UiAutoTests.Extensions;
using UiAutoTests.Locators;

namespace UiAutoTests.Helpers
{
    public class AboutAppWindowHelper
    {
        private readonly Window _window;
        private readonly ConditionFactory _conditionFactory;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();
        private AboutAppWindowLocators _aboutAppLocators;


        public AboutAppWindowHelper(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
            _aboutAppLocators = new(_window, _conditionFactory);
        }



        public void CloseAboutAppWindow()
        {
            _loggerHelper.LogEnteringTheMethod();

            var okButton = _aboutAppLocators.AboutAppOkButton;
            okButton.Invoke();
            _logger.Info("OK button is Invoked");
        }

        public Window GetAboutAppWindow()
        {
            _loggerHelper.LogEnteringTheMethod();

            using (var automation = new UIA3Automation())
            {
                var desktop = automation.GetDesktop();
                var aboutWindow = desktop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByAutomationId("AboutAppView")));

                return aboutWindow?.AsWindow();
            }
        }

        public Window GetMainWindowView(UIA3Automation automation)
        {
            _loggerHelper.LogEnteringTheMethod();

            var desktop = automation.GetDesktop();
            var aboutWindow = desktop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByAutomationId("UserRegistrationView")));

            return aboutWindow?.AsWindow();
        }

        public void Pause(int timeInSecond)
        {
            _loggerHelper.LogEnteringTheMethod();

            WaitExtensions.Pause(timeInSecond);
        }









    }
}
