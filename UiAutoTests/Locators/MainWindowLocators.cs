using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Exceptions;

namespace UiAutoTests.Locators
{
    internal class MainWindowLocators
    {
        private readonly Window _window;
        private readonly ConditionFactory _conditionFactory;


        public MainWindowLocators(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
        }

        private AutomationElement Find(string automationId) =>
            _window.FindFirstDescendant(_conditionFactory.ByAutomationId(automationId))
            ?? throw new ElementNotAvailableException($"Element with AutomationId - [{automationId}] not found");


        public AutomationElement MainWindowsLocator => Find("UserRegistrationView");

        public TextBox UserIdTextBox => Find("UserIdTextBox").AsTextBox();
        public TextBox UserLastNameTextBox => Find("UserLastNameTextBox").AsTextBox();
        public TextBox UserMiddleNameTextBox => Find("UserMiddleNameTextBox").AsTextBox();        
        public TextBox UserFirstNameTextBox => Find("UserFirstNameTextBox").AsTextBox();
        public TextBox AdressUserTextBox => Find("AdressUserTextBox").AsTextBox();
        public TextBox PhoneUserTextBox => Find("PhoneUserTextBox").AsTextBox();
        public TextBox InfoUserTextBox => Find("InfoUserTextBox").AsTextBox();
        public TextBox UpdateTextTextBox => Find("UpdateTextTextBox").AsTextBox();

        public CheckBox BirthDateUserCheckBox => Find("BirthDateUserCheckBox").AsCheckBox();
        public DateTimePicker UserBirthDateDatePicker => Find("UserBirthDateDatePicker").AsDateTimePicker();
        public ComboBox GenderUserComboBox => Find("GenderUserComboBox").AsComboBox();

        public Button CleanUpFieldsButton => Find("CleanUpFieldsButton").AsButton();
        public Button RegistrationUserButton => Find("RegistrationUserButton").AsButton();
    }
}
