using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;

namespace UiAutoTests.Locators
{
    internal class MainWindowLocators
    {
        static Window _window;
        static ConditionFactory _conditionFactory;


        public MainWindowLocators(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
        }



        public AutomationElement MainWindowsLocator => _window.FindFirstDescendant(_conditionFactory.ByAutomationId("UserRegistrationView"));

        public AutomationElement UserIdTextBox => _window.FindFirstDescendant(_conditionFactory.ByAutomationId("UserIdTextBox")).AsTextBox();

        public AutomationElement CurrenciesCombobox => _window.FindFirstDescendant(_conditionFactory.ByAutomationId("CurrenciesCombobox_AId")).AsComboBox();
        public AutomationElement LeveragesCombobox => _window.FindFirstDescendant(_conditionFactory.ByAutomationId("LeveragesCombobox_AId")).AsComboBox();
        public AutomationElement AccountTypesCombobox => _window.FindFirstDescendant(_conditionFactory.ByAutomationId("AccountTypesCombobox_AId")).AsComboBox();

        public Button CleanUpFieldsButton => _window.FindFirstDescendant(_conditionFactory.ByAutomationId("CleanUpFieldsButton")).AsButton();
        public Button RegistrationUserButton => _window.FindFirstDescendant(_conditionFactory.ByAutomationId("RegistrationUserButton")).AsButton();
    }
}
