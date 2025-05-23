using FlaUI.Core.AutomationElements;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiAutoTests.Helpers;

namespace UiAutoTests.Extensions
{
    public static class TextBoxExtensions
    {

        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();


        public static bool IsTextBoxEnabled(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();
            var result = textBox.IsEnabled;

            _logger.Info($"TextBox.Enabled = {result}");
            return result;
        }

        public static void FocusTextBoxAndSetCursor(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();
            textBox.Focus();
            //textBox.Click();

            _logger.Info("TextBox получил фокус");
        }

        public static string GetPlaceholder(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();
            var placeholder = textBox.HelpText ?? string.Empty;

            _logger.Info($"Получен placeholder: \"{placeholder}\"");
            return placeholder;
        }

        public static void EnterText(this TextBox automationElement, string text)
        {
            _loggerHelper.LogEnteringTheMethod(); 

            var textBox = automationElement.EnsureTextBox();
            if (!textBox.IsEnabled)
                throw new InvalidOperationException("TextBox is disabled");

            textBox.Text = text;

            _logger.Info($"Введён текст: \"{text}\"");
        }

        public static string GetText(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();
            var currentText = textBox.Text;

            _logger.Info($"Получен текст: \"{currentText}\"");
            return currentText;
        }

        public static void ClearText(this TextBox automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();
            textBox.Text = string.Empty;

            _logger.Info("Текст в TextBox очищен");
        }













    }
}
