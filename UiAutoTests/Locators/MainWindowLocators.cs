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

        private AutomationElement FindFirstById(string automationId) =>
            _window.FindFirstDescendant(_conditionFactory.ByAutomationId(automationId))
            ?? throw new ElementNotAvailableException($"Element with AutomationId - [{automationId}] not found");

        public AutomationElement[] FindAllById(string automationId) =>
           _window.FindAllDescendants(_conditionFactory.ByAutomationId(automationId))
           ?? throw new ElementNotAvailableException($"Elements with AutomationId - [{automationId}] not found");

        private AutomationElement FindFirstByClassName(string className) =>
            _window.FindFirstDescendant(_conditionFactory.ByClassName(className))
            ?? throw new ElementNotAvailableException($"Element with ClassName - [{className}] not found");

        private AutomationElement[] FindAllByClassName(string className) =>
            _window.FindAllDescendants(_conditionFactory.ByClassName(className))
            ?? throw new ElementNotAvailableException($"Element with ClassName - [{className}] not found");


        public AutomationElement MainWindowsLocator => FindFirstById("UserRegistrationView");

        public TextBox UserIdTextBox => FindFirstById("UserIdTextBox").AsTextBox();
        public TextBox UserLastNameTextBox => FindFirstById("UserLastNameTextBox").AsTextBox();
        public TextBox UserMiddleNameTextBox => FindFirstById("UserMiddleNameTextBox").AsTextBox();        
        public TextBox UserFirstNameTextBox => FindFirstById("UserFirstNameTextBox").AsTextBox();
        public TextBox AddressUserTextBox => FindFirstById("AddressUserTextBox").AsTextBox();
        public TextBox PhoneUserTextBox => FindFirstById("PhoneUserTextBox").AsTextBox();
        public TextBox InfoUserTextBox => FindFirstById("InfoUserTextBox").AsTextBox();

        public CheckBox BirthDateUserCheckBox => FindFirstById("BirthDateUserCheckBox").AsCheckBox();
        public DateTimePicker UserBirthDateDatePicker => FindFirstById("UserBirthDateDatePicker").AsDateTimePicker();
        public ComboBox GenderUserComboBox => FindFirstById("GenderUserComboBox").AsComboBox();

        public Calendar CalendarDate => FindFirstById("CalendarDate").AsCalendar();
        public Button PART_PreviousButton => FindFirstById("PART_PreviousButton").AsButton();
        public Button PART_HeaderButton => FindFirstById("PART_HeaderButton").AsButton();
        public Button PART_NextButton => FindFirstById("PART_NextButton").AsButton();
        //Choose day Name in Button for Example - ("15 июня 2025 г.")
        public AutomationElement CalendarDayButton => FindFirstByClassName("CalendarDayButton"); 
        public AutomationElement[] CalendarDayButtons => FindAllByClassName("CalendarDayButton");
        //Choose month for Example - (Name:	"июль 2025 г.") and year - (Name:	"2020")
        public AutomationElement CalendarButton => FindFirstByClassName("CalendarButton");
        public AutomationElement[] CalendarButtons => FindAllByClassName("CalendarButton");

        public TextBox CalendarDateTextBox => FindFirstById("CalendarDateTextBox").AsTextBox();

        public RadioButton RadioButtonPassport => FindFirstById("RadioButtonPassport").AsRadioButton();
        public RadioButton RadioButtonDriverLicense => FindFirstById("RadioButtonDriverLicense").AsRadioButton();
        public RadioButton RadioButtonIdCard => FindFirstById("RadioButtonIdCard").AsRadioButton();

        public Label SliderLabel => FindFirstById("SliderLabel").AsLabel();
        public Slider SliderCount => FindFirstById("SliderCount").AsSlider();

        public ProgressBar UserGenerationProgressBar => FindFirstById("UserGenerationProgressBar").AsProgressBar();

        public DataGridView UsersCollectionDataGrid => FindFirstById("UsersCollectionDataGrid").AsDataGridView();

        public TextBox UpdateTextTextBox => FindFirstById("UpdateTextTextBox").AsTextBox();

        public Button CleanUpFieldsButton => FindFirstById("CleanUpFieldsButton").AsButton();
        public Button RegistrationUserButton => FindFirstById("RegistrationUserButton").AsButton();

        // Main Menu
        public Menu MainMenu => FindFirstById("MainMenu").AsMenu();

        // Level 1 - Main Items
        public MenuItem HomeMenuItem => FindFirstById("HomeMenuItem").AsMenuItem();
        public MenuItem CoursesMenuItem => FindFirstById("CoursesMenuItem").AsMenuItem();
        public MenuItem TestsMenuItem => FindFirstById("TestsMenuItem").AsMenuItem();
        public MenuItem ProgressMenuItem => FindFirstById("ProgressMenuItem").AsMenuItem();
        public MenuItem CommunityMenuItem => FindFirstById("CommunityMenuItem").AsMenuItem();
        public MenuItem SettingsMenuItem => FindFirstById("SettingsMenuItem").AsMenuItem();
        public MenuItem HelpMenuItem => FindFirstById("HelpMenuItem").AsMenuItem();

        // Level 2 - Courses Submenu
        public MenuItem ProgrammingMenuItem => FindFirstById("ProgrammingMenuItem").AsMenuItem();
        public MenuItem DataScienceMenuItem => FindFirstById("DataScienceMenuItem").AsMenuItem();
        public MenuItem DesignMenuItem => FindFirstById("DesignMenuItem").AsMenuItem();

        // Level 2 - Tests & Exercises Submenu
        public MenuItem CSharpTestMenuItem => FindFirstById("C#TestMenuItem").AsMenuItem();
        public MenuItem PythonTestMenuItem => FindFirstById("PythonTestMenuItem").AsMenuItem();
        public MenuItem PracticalTasksMenuItem => FindFirstById("PracticalTasksMenuItem").AsMenuItem();
        public MenuItem CodeChallengesMenuItem => FindFirstById("CodeChallengesMenuItem").AsMenuItem();

        // Level 2 - Progress Submenu
        public MenuItem AchievementsMenuItem => FindFirstById("AchievementsMenuItem").AsMenuItem();
        public MenuItem StatisticsMenuItem => FindFirstById("StatisticsMenuItem").AsMenuItem();
        public MenuItem CertificatesMenuItem => FindFirstById("CertificatesMenuItem").AsMenuItem();

        // Level 2 - Community Submenu
        public MenuItem ForumMenuItem => FindFirstById("ForumMenuItem").AsMenuItem();
        public MenuItem GroupMenuItem => FindFirstById("GroupMenuItem").AsMenuItem();
        public MenuItem MentorshipMenuItem => FindFirstById("MentorshipMenuItem").AsMenuItem();

        // Level 2 - Settings Submenu
        public MenuItem ProfileMenuItem => FindFirstById("ProfileMenuItem").AsMenuItem();
        public MenuItem NotificationsMenuItem => FindFirstById("NotificationsMenuItem").AsMenuItem();
        public MenuItem AppearanceMenuItem => FindFirstById("AppearanceMenuItem").AsMenuItem();
        public MenuItem SecurityMenuItem => FindFirstById("SecurityMenuItem").AsMenuItem();

        // Level 2 - Help Submenu
        public MenuItem FAQMenuItem => FindFirstById("FAQMenuItem").AsMenuItem();
        public MenuItem FeedbackMenuItem => FindFirstById("FeedbackMenuItem").AsMenuItem();
        public MenuItem AboutMenuItem => FindFirstById("AboutMenuItem").AsMenuItem();

        // Level 3 - Programming Submenu
        public MenuItem CSharpMenuItem => FindFirstById("C#MenuItem").AsMenuItem();
        public MenuItem PythonMenuItem => FindFirstById("PythonMenuItem").AsMenuItem();
        public MenuItem WebMenuItem => FindFirstById("WebMenuItem").AsMenuItem();
        public MenuItem MobileMenuItem => FindFirstById("MobileMenuItem").AsMenuItem();

        // Level 3 - Data Science Submenu
        public MenuItem MachineLearningMenuItem => FindFirstById("MachineLearningMenuItem").AsMenuItem();
        public MenuItem BigDataMenuItem => FindFirstById("BigDataMenuItem").AsMenuItem();
        public MenuItem DataVisualizationMenuItem => FindFirstById("DataVisualizationMenuItem").AsMenuItem();

        // Level 3 - Design Submenu
        public MenuItem UiMenuItem => FindFirstById("UiMenuItem").AsMenuItem();
        public MenuItem GraphicMenuItem => FindFirstById("GraphicMenuItem").AsMenuItem();
        public MenuItem TreeDMenuItem => FindFirstById("3dMenuItem").AsMenuItem();

        public AutomationElement MessageBox => FindFirstByClassName("#32770");  // Стандартный класс MessageBox в Windows
    }
}
