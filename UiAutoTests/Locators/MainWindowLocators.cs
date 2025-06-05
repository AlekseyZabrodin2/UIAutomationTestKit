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

        private AutomationElement FindFirst(string automationId) =>
            _window.FindFirstDescendant(_conditionFactory.ByAutomationId(automationId))
            ?? throw new ElementNotAvailableException($"Element with AutomationId - [{automationId}] not found");

        private AutomationElement[] FindAll(string automationId) =>
           _window.FindAllDescendants(_conditionFactory.ByAutomationId(automationId))
           ?? throw new ElementNotAvailableException($"Element with AutomationId - [{automationId}] not found");


        public AutomationElement MainWindowsLocator => FindFirst("UserRegistrationView");

        public TextBox UserIdTextBox => FindFirst("UserIdTextBox").AsTextBox();
        public TextBox UserLastNameTextBox => FindFirst("UserLastNameTextBox").AsTextBox();
        public TextBox UserMiddleNameTextBox => FindFirst("UserMiddleNameTextBox").AsTextBox();        
        public TextBox UserFirstNameTextBox => FindFirst("UserFirstNameTextBox").AsTextBox();
        public TextBox AdressUserTextBox => FindFirst("AdressUserTextBox").AsTextBox();
        public TextBox PhoneUserTextBox => FindFirst("PhoneUserTextBox").AsTextBox();
        public TextBox InfoUserTextBox => FindFirst("InfoUserTextBox").AsTextBox();

        public CheckBox BirthDateUserCheckBox => FindFirst("BirthDateUserCheckBox").AsCheckBox();
        public DateTimePicker UserBirthDateDatePicker => FindFirst("UserBirthDateDatePicker").AsDateTimePicker();
        public ComboBox GenderUserComboBox => FindFirst("GenderUserComboBox").AsComboBox();

        public Calendar CalendarDate => FindFirst("CalendarDate").AsCalendar();
        public TextBox CalendarDateTextBox => FindFirst("CalendarDateTextBox").AsTextBox();

        public RadioButton RadioButtonPassport => FindFirst("RadioButtonPassport").AsRadioButton();
        public RadioButton RadioButtonDriverLicense => FindFirst("RadioButtonDriverLicense").AsRadioButton();
        public RadioButton RadioButtonIdCard => FindFirst("RadioButtonIdCard").AsRadioButton();

        public Label SliderLabel => FindFirst("SliderLabel").AsLabel();
        public Slider SliderCount => FindFirst("SliderCount").AsSlider();

        public ProgressBar UserGenerationProgressBar => FindFirst("UserGenerationProgressBar").AsProgressBar();

        public DataGridView UsersCollectionDataGrid => FindFirst("UsersCollectionDataGrid").AsDataGridView();

        public TextBox UpdateTextTextBox => FindFirst("UpdateTextTextBox").AsTextBox();

        public Button CleanUpFieldsButton => FindFirst("CleanUpFieldsButton").AsButton();
        public Button RegistrationUserButton => FindFirst("RegistrationUserButton").AsButton();
    }
}
