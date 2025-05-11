using FlaUI.Core.AutomationElements;
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



        public static bool IsTextBoxEnabled(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();

            return textBox.IsEnabled; 
        }

        public static void FocusTextBoxAndSetCursor(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();
            textBox.Focus();
            //textBox.Click();
        }

        public static string GetPlaceholder(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();

            return textBox.HelpText ?? string.Empty;
        }

        public static void EnterText(this AutomationElement automationElement, string text)
        {
            _loggerHelper.LogEnteringTheMethod(); 

            var textBox = automationElement.EnsureTextBox();
            if (!textBox.IsEnabled)
                throw new InvalidOperationException("TextBox is disabled");

            textBox.Text = text;
        }

        public static string GetText(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();

            return textBox.Text;
        }

        public static void ClearText(this AutomationElement automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = automationElement.EnsureTextBox();

            textBox.Text = string.Empty;
        }













    }
}
