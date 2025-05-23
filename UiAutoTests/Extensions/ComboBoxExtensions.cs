using FlaUI.Core.AutomationElements;
using NLog;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class ComboBoxExtensions
    {

        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();



        public static void SelectItem(this ComboBox automationElement, int index)
        {
            _loggerHelper.LogEnteringTheMethod();

            var comboBox = automationElement.EnsureComboBox();

            _logger.Info("select item = [{0}]", index);
            comboBox.Select(index);
        }
    }
}
