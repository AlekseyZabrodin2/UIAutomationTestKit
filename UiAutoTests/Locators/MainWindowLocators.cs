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
           ?? throw new ElementNotAvailableException($"Elements with AutomationId - [{automationId}] not found");

        private AutomationElement FindFirstByClassName(string className) =>
            _window.FindFirstDescendant(_conditionFactory.ByClassName(className))
            ?? throw new ElementNotAvailableException($"Element with ClassName - [{className}] not found");

        private AutomationElement[] FindAllByClassName(string className) =>
            _window.FindAllDescendants(_conditionFactory.ByClassName(className))
            ?? throw new ElementNotAvailableException($"Element with ClassName - [{className}] not found");


        public AutomationElement MainWindowsLocator => FindFirst("UserRegistrationView");

        public TextBox UserIdTextBox => FindFirst("UserIdTextBox").AsTextBox();
        public TextBox UserLastNameTextBox => FindFirst("UserLastNameTextBox").AsTextBox();
        public TextBox UserMiddleNameTextBox => FindFirst("UserMiddleNameTextBox").AsTextBox();        
        public TextBox UserFirstNameTextBox => FindFirst("UserFirstNameTextBox").AsTextBox();
        public TextBox AddressUserTextBox => FindFirst("AddressUserTextBox").AsTextBox();
        public TextBox PhoneUserTextBox => FindFirst("PhoneUserTextBox").AsTextBox();
        public TextBox InfoUserTextBox => FindFirst("InfoUserTextBox").AsTextBox();

        public CheckBox BirthDateUserCheckBox => FindFirst("BirthDateUserCheckBox").AsCheckBox();
        public DateTimePicker UserBirthDateDatePicker => FindFirst("UserBirthDateDatePicker").AsDateTimePicker();
        public ComboBox GenderUserComboBox => FindFirst("GenderUserComboBox").AsComboBox();

        public Calendar CalendarDate => FindFirst("CalendarDate").AsCalendar();
        public Button PART_PreviousButton => FindFirst("PART_PreviousButton").AsButton();
        public Button PART_HeaderButton => FindFirst("PART_HeaderButton").AsButton();
        public Button PART_NextButton => FindFirst("PART_NextButton").AsButton();
        //Choose day Name in Button for Example - ("15 июня 2025 г.")
        public AutomationElement CalendarDayButton => FindFirstByClassName("CalendarDayButton"); 
        public AutomationElement[] CalendarDayButtons => FindAllByClassName("CalendarDayButton");
        //Choose month for Example - (Name:	"июль 2025 г.") and year - (Name:	"2020")
        public AutomationElement CalendarButton => FindFirstByClassName("CalendarButton");
        public AutomationElement[] CalendarButtons => FindAllByClassName("CalendarButton");

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

        // Main Menu
        public Menu MainMenu => FindFirst("MainMenu").AsMenu();

        // Level 1 - Main Items
        public MenuItem HomeMenuItem => FindFirst("HomeMenuItem").AsMenuItem();
        public MenuItem CoursesMenuItem => FindFirst("CoursesMenuItem").AsMenuItem();
        public MenuItem TestsMenuItem => FindFirst("TestsMenuItem").AsMenuItem();
        public MenuItem ProgressMenuItem => FindFirst("ProgressMenuItem").AsMenuItem();
        public MenuItem CommunityMenuItem => FindFirst("CommunityMenuItem").AsMenuItem();
        public MenuItem SettingsMenuItem => FindFirst("SettingsMenuItem").AsMenuItem();
        public MenuItem HelpMenuItem => FindFirst("HelpMenuItem").AsMenuItem();

        // Level 2 - Courses Submenu
        public MenuItem ProgrammingMenuItem => FindFirst("ProgrammingMenuItem").AsMenuItem();
        public MenuItem DataScienceMenuItem => FindFirst("DataScienceMenuItem").AsMenuItem();
        public MenuItem DesignMenuItem => FindFirst("DesignMenuItem").AsMenuItem();

        // Level 2 - Tests & Exercises Submenu
        public MenuItem CSharpTestMenuItem => FindFirst("C#TestMenuItem").AsMenuItem();
        public MenuItem PythonTestMenuItem => FindFirst("PythonTestMenuItem").AsMenuItem();
        public MenuItem PracticalTasksMenuItem => FindFirst("PracticalTasksMenuItem").AsMenuItem();
        public MenuItem CodeChallengesMenuItem => FindFirst("CodeChallengesMenuItem").AsMenuItem();

        // Level 2 - Progress Submenu
        public MenuItem AchievementsMenuItem => FindFirst("AchievementsMenuItem").AsMenuItem();
        public MenuItem StatisticsMenuItem => FindFirst("StatisticsMenuItem").AsMenuItem();
        public MenuItem CertificatesMenuItem => FindFirst("CertificatesMenuItem").AsMenuItem();

        // Level 2 - Community Submenu
        public MenuItem ForumMenuItem => FindFirst("ForumMenuItem").AsMenuItem();
        public MenuItem GroupMenuItem => FindFirst("GroupMenuItem").AsMenuItem();
        public MenuItem MentorshipMenuItem => FindFirst("MentorshipMenuItem").AsMenuItem();

        // Level 2 - Settings Submenu
        public MenuItem ProfileMenuItem => FindFirst("ProfileMenuItem").AsMenuItem();
        public MenuItem NotificationsMenuItem => FindFirst("NotificationsMenuItem").AsMenuItem();
        public MenuItem AppearanceMenuItem => FindFirst("AppearanceMenuItem").AsMenuItem();
        public MenuItem SecurityMenuItem => FindFirst("SecurityMenuItem").AsMenuItem();

        // Level 2 - Help Submenu
        public MenuItem FAQMenuItem => FindFirst("FAQMenuItem").AsMenuItem();
        public MenuItem FeedbackMenuItem => FindFirst("FeedbackMenuItem").AsMenuItem();
        public MenuItem AboutMenuItem => FindFirst("AboutMenuItem").AsMenuItem();

        // Level 3 - Programming Submenu
        public MenuItem CSharpMenuItem => FindFirst("C#MenuItem").AsMenuItem();
        public MenuItem PythonMenuItem => FindFirst("PythonMenuItem").AsMenuItem();
        public MenuItem WebMenuItem => FindFirst("WebMenuItem").AsMenuItem();
        public MenuItem MobileMenuItem => FindFirst("MobileMenuItem").AsMenuItem();

        // Level 3 - Data Science Submenu
        public MenuItem MachineLearningMenuItem => FindFirst("MachineLearningMenuItem").AsMenuItem();
        public MenuItem BigDataMenuItem => FindFirst("BigDataMenuItem").AsMenuItem();
        public MenuItem DataVisualizationMenuItem => FindFirst("DataVisualizationMenuItem").AsMenuItem();

        // Level 3 - Design Submenu
        public MenuItem UiMenuItem => FindFirst("UiMenuItem").AsMenuItem();
        public MenuItem GraphicMenuItem => FindFirst("GraphicMenuItem").AsMenuItem();
        public MenuItem TreeDMenuItem => FindFirst("3dMenuItem").AsMenuItem();
    }
}
