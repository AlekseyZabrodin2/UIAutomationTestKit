using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using NLog;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Locators;

namespace UiAutoTests.Controllers
{
    internal class MainWindowController : IClientState
    {
        private readonly Window _window;
        private readonly ConditionFactory _conditionFactory;
        private MainWindowLocators _mainWindowStateLocators;
        private LoggerHelper _loggerHelper = new();
        private MainWindowHelper _mainWindowManager;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public string Name { get; } = "MainWindowState";


        public MainWindowController(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
            _mainWindowStateLocators = new(_window, _conditionFactory);
            _mainWindowManager = new(_window, _conditionFactory);
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


        //public void CreateAccounte()
        //{
        //    _mainWindowManager.CreateAccounte();
        //}

        //public void SelectDepositItem(object? index)
        //{
        //    _mainWindowManager.SelectDepositItem(index);
        //}

        //public void SelectAccountType(int index)
        //{
        //    _mainWindowManager.SelectAccountType(index);
        //}

        //public void SelectLeverages(int index)
        //{
        //    _mainWindowManager.SelectLeverages(index);
        //}

        //public void SelectCurrencies(int index)
        //{
        //    _mainWindowManager.SelectCurrencies(index);
        //}

        //public bool ValidationDeposits()
        //{
        //    return _mainWindowManager.ValidationDepositsCombobox();
        //}

        //public bool CheckingCreateAccountButtonIsEnabled()
        //{
        //    return _mainWindowManager.CheckingCreateAccountButtonIsEnabled();
        //}



        //public void WaitingBetweenCommand(int waitingTime)
        //{
        //    _mainWindowManager.WaitingBetweenCommand(waitingTime);
        //}


    }
}
