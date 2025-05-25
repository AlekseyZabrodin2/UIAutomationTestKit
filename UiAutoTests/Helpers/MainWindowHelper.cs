using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using NLog;
using UiAutoTests.Extensions;
using UiAutoTests.Locators;

namespace UiAutoTests.Helpers
{
    public class MainWindowHelper
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


        public void SetUserId(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.UserIdTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetLastName(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.UserLastNameTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetMiddleName(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.UserMiddleNameTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetFirstName(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.UserFirstNameTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }


        public void CheckedBirthdate()
        {
            _loggerHelper.LogEnteringTheMethod();

            var checkBox = _mainWindowLocators.BirthDateUserCheckBox;
            checkBox.CheckBoxChecked();
        }

        public void UncheckedBirthdate()
        {
            _loggerHelper.LogEnteringTheMethod();

            var checkBox = _mainWindowLocators.BirthDateUserCheckBox;
            checkBox.CheckBoxUnchecked();
        }

        public void SetBirthdateText(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var datePicker = _mainWindowLocators.UserBirthDateDatePicker;
            datePicker.Focus();
            datePicker.SelectedDate = DateTime.Parse(inputText);
        }

        public void SelectGender(int genderIndex)
        {
            _loggerHelper.LogEnteringTheMethod();

            var comboBox = _mainWindowLocators.GenderUserComboBox;
            comboBox.SelectItemByIndex(genderIndex);
        }

        public void SetAdressUser(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.AdressUserTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetPhoneUser(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.PhoneUserTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetInfoUser(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.InfoUserTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void ClickCleanButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var cleanUpButton = _mainWindowLocators.CleanUpFieldsButton;
            cleanUpButton.ClickButton();
        }

        public void ClickRegistrationButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var cleanUpButton = _mainWindowLocators.RegistrationUserButton;
            cleanUpButton.ClickButton();
        }

        public bool IsRegistrationButtonEnabled()
        {
            _loggerHelper.LogEnteringTheMethod();

            var cleanUpButton = _mainWindowLocators.RegistrationUserButton;
            return cleanUpButton.IsButtonEnabled();
        }

        public string GetTextFromUserIdTextBox()
        {
            _loggerHelper.LogEnteringTheMethod();

            var userIdTextBox = _mainWindowLocators.UserIdTextBox;

            return userIdTextBox.GetText();
        }




        public void Pause(int timeInSecond)
        {
            _loggerHelper.LogEnteringTheMethod();

            WaitExtensions.Pause(timeInSecond);
        }

        public void WaitUntilTextIsEmpty(int timeInSecond)
        {
            _loggerHelper.LogEnteringTheMethod();

            var userIdTextBox = _mainWindowLocators.UserIdTextBox;
            userIdTextBox.WaitUntilTextIsEmpty(timeInSecond);
        }




    }
}
