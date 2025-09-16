using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using NLog;
using Polly;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Locators;

namespace UiAutoTests.Controllers
{
    public class AboutAppWindowController : BaseController, IClientState
    {
        private readonly Window _window;
        private readonly ConditionFactory _conditionFactory;
        private AboutAppWindowLocators _aboutAppLocators;        
        private static AboutAppWindowHelper _aboutAppWindowHelper;
        private LoggerHelper _loggerHelper = new();
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private TimeSpan _nextStateDelay = TimeSpan.FromSeconds(1);
        private const int _nextStateRetry = 30;

        public string Name { get; } = "AboutAppWindowState";


        public AboutAppWindowController(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
            _aboutAppLocators = new(_window, _conditionFactory);
            _aboutAppWindowHelper = new(_window, _conditionFactory);
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
                    state = state.ToNextState();
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

        public IClientState ToNextState()
        {
            _loggerHelper.LogEnteringTheMethod();

            IClientState mainWindowState = null;

            CloseAboutAppWindow();

            var policy = Policy.Handle<Exception>()
                .WaitAndRetry(_nextStateRetry, retryAttempt => _nextStateDelay);

            policy.Execute(() =>
            {
                using (var automation = new UIA3Automation())
                {
                    var mainWindow = _aboutAppWindowHelper.GetMainWindowView(automation);
                    mainWindowState = new AboutAppWindowController(mainWindow, _conditionFactory);
                    if (!mainWindowState.IsState(mainWindow))
                    {
                        throw new Exception("Can not follow to the MainWindow state");
                    }
                }
            });

            return mainWindowState;
        }

        public bool IsState(Window window)
        {
            _loggerHelper.LogEnteringTheMethod();

            var result = false;

            var okButton = _aboutAppLocators.AboutAppOkButton;
            if (okButton != null)
            {
                result = true;
            }

            _logger.Debug($"AboutAppWindow State - [{result}]");
            return result;
        }



        public Window GetAboutAppWindow()
        {
            return _aboutAppWindowHelper.GetAboutAppWindow();
        }

        public Window GetMainWindowView()
        {
            using (var automation = new UIA3Automation())
            {
                return _aboutAppWindowHelper.GetMainWindowView(automation);
            }
        }

        public AboutAppWindowController CloseAboutAppWindow()
        {
            _aboutAppWindowHelper.CloseAboutAppWindow();
            return this;
        }

        public AboutAppWindowController Pause(int time)
        {
            _aboutAppWindowHelper.Pause(time);
            return this;
        }
    }
}
