using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace UiAutoTests.Extensions
{
    public static class AutomationElementChecks
    {


        public static Button EnsureButton(this AutomationElement automationElement)
        {
            var button = automationElement.AsButton();
            if (button == null || button.ControlType != ControlType.Button)
                throw new ArgumentException("Element is not a Button.");

            return button;
        }

        public static CheckBox EnsureCheckBox(this AutomationElement automationElement)
        {
            var checkBox = automationElement.AsCheckBox();
            if (checkBox == null || checkBox.ControlType != ControlType.CheckBox)
                throw new ArgumentException("Element is not a CheckBox.");

            return checkBox;
        }

        public static ComboBox EnsureComboBox(this AutomationElement automationElement)
        {
            var comboBox = automationElement.AsComboBox();
            if (comboBox == null || comboBox.ControlType != ControlType.ComboBox)
                throw new ArgumentException("Element is not a ComboBox.");

            return comboBox;
        }

        public static TextBox EnsureTextBox(this AutomationElement automationElement)
        {
            var textBox = automationElement.AsTextBox();
            if (textBox == null || textBox.ControlType != ControlType.Edit)
                throw new ArgumentException("Element is not a TextBox.");

            return textBox;
        }






    }
}
