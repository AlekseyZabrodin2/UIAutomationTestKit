using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using NLog;
using Polly;
using System.Diagnostics;
using UiAutoTests.Core;
using UiAutoTests.Locators;

namespace UiAutoTests.Services
{
    internal class AutomationTestClient : ITestClient
    {
        private readonly string _programPath;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();
        private TimeSpan _retryDelay = TimeSpan.FromSeconds(1);
        private Application _application;
        private Window _window;
        private ConditionFactory _conditionFactory;
        private MainWindowLocators _mainWindowLocators;


        public AutomationTestClient(string programPath)
        {
            _programPath = programPath;
        }



        public async Task<IClientState> StartAsync(TimeSpan timeout)
        {
            _loggerHelper.LogEnteringTheMethod();

            var retryCounts = (int)(timeout.TotalMilliseconds / _retryDelay.TotalMilliseconds);

            var startInfo = new ProcessStartInfo { FileName = _programPath, UseShellExecute = true, CreateNoWindow = false };
            _application = Application.Launch(startInfo);

            var mainWindowWaitPolicy = Policy.Handle<Exception>()
               .WaitAndRetryAsync(retryCounts, retryAttempt => _retryDelay);

            await mainWindowWaitPolicy.ExecuteAsync(async () =>
            {
                _logger.Trace("Try to find Main window");

                var automation = new UIA3Automation();
                var windows = _application.GetAllTopLevelWindows(automation);
                _conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());

                if (windows.All(w => w.FrameworkType != FrameworkType.Wpf))
                {
                    _logger.Error("Main window did not find");
                    throw new Exception("Main window did not find");
                }

                _window = windows.Single(w => w.FrameworkType == FrameworkType.Wpf);
                await Task.CompletedTask;
                _logger.Trace("Main window is found");

            });

            _logger.Trace("Wait for MainWindow state");

            _mainWindowLocators = new(_window, _conditionFactory);

            var menuWindowWaitPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(retryCounts, retryAttempt => _retryDelay);
            await menuWindowWaitPolicy.ExecuteAsync(async () =>
            {
                _logger.Trace("Try to find CreateAccountWindows");

                var createAccountWindows = _mainWindowLocators.MainWindowsLocator.AsWindow();
                createAccountWindows.DrawHighlight(); // For demonstrations

                if (createAccountWindows == null)
                {
                    _logger.Error("Can`t go to MainWindow State");
                    throw new Exception("Window did not find");
                }

                await Task.CompletedTask;
                _logger.Trace("CreateAccountWindows is found");
            });

            _logger.Debug("CreateAccountTestClient Started");
            return new MainWindowState(_window, _conditionFactory);
        }

        public void Kill()
        {
            _logger.Trace($"CreateAccountTestClient is Kill");
            _application.Kill();
        }
    }
}
