using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Exceptions;

namespace UiAutoTests.Locators
{
    internal class AboutAppWindowLocators
    {
        private readonly Window _aboutWindow;
        private readonly ConditionFactory _conditionFactory;


        public AboutAppWindowLocators(Window window, ConditionFactory conditionFactory)
        {
            _aboutWindow = window;
            _conditionFactory = conditionFactory;
        }

        private AutomationElement FindFirstById(string automationId) =>
             _aboutWindow.FindFirstDescendant(_conditionFactory.ByAutomationId(automationId))
             ?? throw new ElementNotAvailableException($"Element with AutomationId - [{automationId}] not found");


        public Window AboutAppView => FindFirstById("AboutAppView").AsWindow();
        public Label AboutAppWindowTitle => FindFirstById("AboutAppWindowTitle").AsLabel();
        public Button AboutAppOkButton => FindFirstById("AboutAppOkButton").AsButton();

    }
}
