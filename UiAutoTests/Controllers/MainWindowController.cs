using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using NLog;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Locators;

namespace UiAutoTests.Controllers
{
    public class MainWindowController : IClientState
    {
        private readonly Window _window;
        private readonly ConditionFactory _conditionFactory;
        private MainWindowLocators _mainWindowStateLocators;
        private LoggerHelper _loggerHelper = new();
        private static MainWindowHelper _mainWindowHelper;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public string Name { get; } = "MainWindowState";


        public MainWindowController(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
            _mainWindowStateLocators = new(_window, _conditionFactory);
            _mainWindowHelper = new(_window, _conditionFactory);
        }


        public async Task<IClientState> GoToStateAsync(string stateName, TimeSpan timeout)
        {
            _loggerHelper.LogEnteringTheMethod();

            IClientState state = null;

            var cancellationTokenSource = new CancellationTokenSource();

            var toStateTask = Task.Run(() =>
            {
                state = this;
                while (!state.Name.Equals(stateName))
                {
                    state = this;
                }

            }, cancellationTokenSource.Token);

            var task = await Task.WhenAny(toStateTask, Task.Delay(timeout));
            if (task != toStateTask)
            {
                cancellationTokenSource.Cancel();
            }

            return state;
        }

        public Window GetMainWindow()
        {
            _loggerHelper.LogEnteringTheMethod();
            return _window;
        }

        public bool IsState(Window window)
        {
            _loggerHelper.LogEnteringTheMethod();

            var result = false;

            var createButton = _mainWindowStateLocators.RegistrationUserButton;
            if (createButton != null)
            {
                result = true;
            }

            _logger.Debug($"MainWindow State - [{result}]");
            return result;
        }


        public MainWindowController SetUserId(string inputText)
        {
            _mainWindowHelper.SetUserId(inputText);
            return this;
        }

        public MainWindowController SetLastName(string inputText)
        {
            _mainWindowHelper.SetLastName(inputText);
            return this;
        }

        public MainWindowController SetMiddleName(string inputText)
        {
            _mainWindowHelper.SetMiddleName(inputText);
            return this;
        }

        public MainWindowController SetFirstName(string inputText)
        {
            _mainWindowHelper.SetFirstName(inputText);
            return this;
        }

        public MainWindowController CheckedBirthdate()
        {
            _mainWindowHelper.CheckedBirthdate();
            return this;
        }

        public MainWindowController UncheckedBirthdate()
        {
            _mainWindowHelper.UncheckedBirthdate();
            return this;
        }

        public MainWindowController SetBirthdateText(string inputText)
        {
            _mainWindowHelper.SetBirthdateText(inputText);
            return this;
        }

        public MainWindowController SelectGender(int genderIndex)
        {
            _mainWindowHelper.SelectGender(genderIndex);
            return this;
        }

        public MainWindowController SetAdressUser(string inputText)
        {
            _mainWindowHelper.SetAdressUser(inputText);
            return this;
        }

        public MainWindowController SetPhoneUser(string inputText)
        {
            _mainWindowHelper.SetPhoneUser(inputText);
            return this;
        }

        public MainWindowController SetInfoUser(string inputText)
        {
            _mainWindowHelper.SetInfoUser(inputText);
            return this;
        }

        public MainWindowController Pause(int time)
        {
            _mainWindowHelper.Pause(time);
            return this;
        }

        public MainWindowController WaitUntilTextIsEmpty(int time)
        {
            _mainWindowHelper.WaitUntilTextIsEmpty(time);
            return this;
        }

        public MainWindowController ClickCleanButton()
        {
            _mainWindowHelper.ClickCleanButton();
            return this;
        }

        public MainWindowController ClickRegistrationButton()
        {
            _mainWindowHelper.ClickRegistrationButton();
            return this;
        }

        public MainWindowController GetTextFromUserIdTextBox()
        {
            GetUserIdText();
            return this;
        }

        public string GetUserIdText()
        {            
            return _mainWindowHelper.GetTextFromUserIdTextBox();
        }

        public bool IsRegistrationButtonEnabled()
        {
            return _mainWindowHelper.IsRegistrationButtonEnabled();
        }

        public MainWindowController SetValidDataInUserForm(int genderCount, int userCount)
        {
            _mainWindowHelper.SetValidDataInUserForm(genderCount, userCount);
            return this;
        }

        public MainWindowController RegistrationSeveralUsers(int count)
        {
            _mainWindowHelper.RegistrationSeveralUsers(count);
            return this;
        }

        public MainWindowController GetDataGridRowCount()
        {
            GetRowCount();
            return this;
        }

        public int GetRowCount()
        {            
            return _mainWindowHelper.GetRowCountInDataGrid();
        }
    }
}
