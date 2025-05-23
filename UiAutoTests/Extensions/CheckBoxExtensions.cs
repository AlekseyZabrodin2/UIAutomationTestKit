using FlaUI.Core.AutomationElements;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class CheckBoxExtensions
    {
        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();



        public static void CheckBoxChecked(this CheckBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var checkBox = automationElement.EnsureCheckBox();

            _logger.Info("current state = [{0}]", checkBox.IsChecked);
            if (checkBox.IsChecked != true)
            {
                checkBox.IsChecked = true;
                _logger.Info("CheckBox set to Checked.");
            }
            else
            {
                _logger.Info("CheckBox is already Checked.");
            }
        }

        public static void CheckBoxUnchecked(this CheckBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var checkBox = automationElement.EnsureCheckBox();

            _logger.Info("current state = [{0}]", checkBox.IsChecked);
            if (checkBox.IsChecked != false)
            {
                checkBox.IsChecked = false;
                _logger.Info("CheckBox set to Unchecked.");
            }
            else
            {
                _logger.Info("CheckBox is already Unchecked.");
            }
        }

        public static bool CheckBoxIsEnabled(this CheckBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var checkBox = automationElement.EnsureCheckBox();
            var state = checkBox.IsEnabled;

            _logger.Info($"[{checkBox.AutomationId}] is [{state}]");

            return state;
        }

        public static bool CheckBoxIsChecked(this CheckBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var checkBox = automationElement.EnsureCheckBox();
            bool? state = checkBox.IsChecked;

            _logger.Info($"[{checkBox.AutomationId}] is [{state}]");

            return state == true;
        }
    }
}
