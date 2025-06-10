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

        public void ClickCalendarPreviousButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var previousButton = _mainWindowLocators.PART_PreviousButton;
            previousButton.ClickButton();
        }

        public void ClickCalendarHeaderButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var headerButton = _mainWindowLocators.PART_HeaderButton;
            headerButton.ClickButton();

        }

        public void ClickCalendarNextButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var nextButton = _mainWindowLocators.PART_NextButton.AsButton();
            nextButton.ClickButton();
        }

        public void ClickCalendarDayButton(string buttonName)
        {
            _loggerHelper.LogEnteringTheMethod();

            var calendarDayButton = _mainWindowLocators.CalendarDayButtons;
            var button = calendarDayButton.FirstOrDefault(name => name.Name == buttonName).AsButton();
            button.ClickButton();
        }

        public RadioButton[] GetAllRadioButtons()
        {
            _loggerHelper.LogEnteringTheMethod();

            return new[]
            {
                _mainWindowLocators.RadioButtonPassport,
                _mainWindowLocators.RadioButtonDriverLicense,
                _mainWindowLocators.RadioButtonIdCard
            };
        }

        public void SelectRandomRadioButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var radioButtons = GetAllRadioButtons();

            var random = new Random();
            var result = random.Next(0, radioButtons.Length);
            var button = radioButtons[result];

            button.SelectRadioButton();
        }

        public void SelectRadioButtonByIndex(int index)
        {
            _loggerHelper.LogEnteringTheMethod();

            var radioButtons = GetAllRadioButtons();
            var button = radioButtons[index];

            button.SelectRadioButton();
        }

        public void SelectUserCountBySlider(int count)
        {
            _loggerHelper.LogEnteringTheMethod();

            var slider = _mainWindowLocators.SliderCount;
            slider.SetValue(count);
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

            var regButton = _mainWindowLocators.RegistrationUserButton;
            regButton.ClickButton();
        }

        public void WaitUntilProgressBarIs(int prograssBarValue)
        {
            _loggerHelper.LogEnteringTheMethod();

            var progressBar = _mainWindowLocators.UserGenerationProgressBar;
            progressBar.WaitUntilValueIs(prograssBarValue);
        }

        public bool IsRegistrationButtonEnabled()
        {
            _loggerHelper.LogEnteringTheMethod();

            var button = _mainWindowLocators.RegistrationUserButton;
            return button.IsButtonEnabled();
        }

        public string GetTextFromUserIdTextBox()
        {
            _loggerHelper.LogEnteringTheMethod();

            var userIdTextBox = _mainWindowLocators.UserIdTextBox;

            return userIdTextBox.GetText();
        }

        public void RegistrationSeveralUsers(int count)
        {
            _loggerHelper.LogEnteringTheMethod();

            SetValidDataInUserForm(2, count);
            ClickRegistrationButton();
            WaitUntilProgressBarIs(count);
        }

        public int GetRowCountInDataGrid()
        {
            _loggerHelper.LogEnteringTheMethod();

            var dataGrid = _mainWindowLocators.UsersCollectionDataGrid;
            var result = dataGrid.GetRowCount();

            return result;
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

        public void SetValidDataInUserForm(int genderCount, int userCount)
        {
            _loggerHelper.LogEnteringTheMethod();

            SetUserId("1");
            SetLastName("Ivanov");
            SetMiddleName("Ivan");
            SetFirstName("Ivanovich");
            CheckedBirthdate();
            SelectGender(genderCount);
            SetBirthdateText("25.12.1995");
            SetAdressUser("London, Baker Street 221B");
            SetPhoneUser("5465431");
            SetInfoUser("Second test case with different data");
            ClickCalendarDayButton("15 июня 2025 г.");
            SelectRandomRadioButton();

            SelectUserCountBySlider(userCount);
        }


    }
}
