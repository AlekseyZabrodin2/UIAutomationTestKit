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

        public static MenuItem EnsureMenuItem(this AutomationElement automationElement)
        {
            var menuItem = automationElement.AsMenuItem();
            if (menuItem == null || menuItem.ControlType != ControlType.MenuItem)
                throw new ArgumentException("Element is not a MenuItem.");

            return menuItem;
        }

        public static Menu EnsureMenu(this AutomationElement automationElement)
        {
            var menu = automationElement.AsMenu();
            if (menu == null || menu.ControlType != ControlType.Menu)
                throw new ArgumentException("Element is not a Menu.");

            return menu;
        }

        public static DataGridView EnsureDataGridView(this AutomationElement automationElement)
        {
            var dataGrid = automationElement.AsDataGridView();
            if (dataGrid == null || dataGrid.ControlType != ControlType.DataGrid)
                throw new ArgumentException("Element is not a DataGridView.");

            return dataGrid;
        }

        public static Tree EnsureTree(this AutomationElement automationElement)
        {
            var tree = automationElement.AsTree();
            if (tree == null || tree.ControlType != ControlType.Tree)
                throw new ArgumentException("Element is not a Tree.");

            return tree;
        }

        public static TreeItem EnsureTreeItem(this AutomationElement automationElement)
        {
            var treeItem = automationElement.AsTreeItem();
            if (treeItem == null || treeItem.ControlType != ControlType.TreeItem)
                throw new ArgumentException("Element is not a TreeItem.");

            return treeItem;
        }

        public static ListBox EnsureListBox(this AutomationElement automationElement)
        {
            var listBox = automationElement.AsListBox();
            if (listBox == null || listBox.ControlType != ControlType.List)
                throw new ArgumentException("Element is not a ListBox.");

            return listBox;
        }

        public static TabItem EnsureTabItem(this AutomationElement automationElement)
        {
            var tabItem = automationElement.AsTabItem();
            if (tabItem == null || tabItem.ControlType != ControlType.TabItem)
                throw new ArgumentException("Element is not a TabItem.");

            return tabItem;
        }

        public static Tab EnsureTab(this AutomationElement automationElement)
        {
            var tab = automationElement.AsTab();
            if (tab == null || tab.ControlType != ControlType.Tab)
                throw new ArgumentException("Element is not a Tab.");

            return tab;
        }

        public static Calendar EnsureCalendar(this AutomationElement automationElement)
        {
            var calendar = automationElement.AsCalendar();
            if (calendar == null || calendar.ControlType != ControlType.Calendar)
                throw new ArgumentException("Element is not a Calendar.");

            return calendar;
        }

        public static DateTimePicker EnsureDateTimePicker(this AutomationElement automationElement)
        {
            var dateTimePicker = automationElement.AsDateTimePicker();
            if (dateTimePicker == null || dateTimePicker.ControlType != ControlType.Custom)
                throw new ArgumentException("Element is not a DateTimePicker.");

            return dateTimePicker;
        }

        public static AutomationElement EnsureToolTip(this AutomationElement automationElement)
        {
            if (automationElement == null || automationElement.ControlType != ControlType.ToolTip)
                throw new ArgumentException("Element is not a ToolTip.");

            return automationElement;
        }

        public static ListBoxItem EnsureListBoxItem(this AutomationElement automationElement)
        {
            var listBoxItem = automationElement.AsListBoxItem();
            if (listBoxItem == null || listBoxItem.ControlType != ControlType.ListItem)
                throw new ArgumentException("Element is not a ListBoxItem.");

            return listBoxItem;
        }

        public static Grid EnsureGrid(this AutomationElement automationElement)
        {
            var grid = automationElement.AsGrid();
            if (grid == null || grid.ControlType != ControlType.Table)
                throw new ArgumentException("Element is not a Grid.");

            return grid;
        }

        public static GridRow EnsureGridRow(this AutomationElement automationElement)
        {
            var gridRow = automationElement.AsGridRow();
            if (gridRow == null || gridRow.ControlType != ControlType.ListItem)
                throw new ArgumentException("Element is not a GridRow.");

            return gridRow;
        }

        public static GridHeader EnsureGridHeader(this AutomationElement automationElement)
        {
            var gridHeader = automationElement.AsGridHeader();
            if (gridHeader == null || gridHeader.ControlType != ControlType.Header)
                throw new ArgumentException("Element is not a GridHeader.");

            return gridHeader;
        }

        public static GridHeaderItem EnsureGridHeaderItem(this AutomationElement automationElement)
        {
            var gridHeaderItem = automationElement.AsGridHeaderItem();
            if (gridHeaderItem == null || gridHeaderItem.ControlType != ControlType.HeaderItem)
                throw new ArgumentException("Element is not a GridHeaderItem.");

            return gridHeaderItem;
        }

        public static Label EnsureLabel(this AutomationElement automationElement)
        {
            var label = automationElement.AsLabel();
            if (label == null || label.ControlType != ControlType.Text)
                throw new ArgumentException("Element is not a Label.");

            return label;
        }

        public static RadioButton EnsureRadioButton(this AutomationElement automationElement)
        {
            var radio = automationElement.AsRadioButton();
            if (radio == null || radio.ControlType != ControlType.RadioButton)
                throw new ArgumentException("Element is not a RadioButton.");

            return radio;
        }

        public static Slider EnsureSlider(this AutomationElement automationElement)
        {
            var slider = automationElement.AsSlider();
            if (slider == null || slider.ControlType != ControlType.Slider)
                throw new ArgumentException("Element is not a Slider.");

            return slider;
        }

        public static ProgressBar EnsureProgressBar(this AutomationElement automationElement)
        {
            var progressBar = automationElement.AsProgressBar();
            if (progressBar == null || progressBar.ControlType != ControlType.ProgressBar)
                throw new ArgumentException("Element is not a ProgressBar.");

            return progressBar;
        }

        public static Spinner EnsureSpinner(this AutomationElement automationElement)
        {
            var spinner = automationElement.AsSpinner();
            if (spinner == null || spinner.ControlType != ControlType.Spinner)
                throw new ArgumentException("Element is not a Spinner.");

            return spinner;
        }
        
        public static Window EnsureWindow(this AutomationElement automationElement)
        {
            var window = automationElement.AsWindow();
            if (window == null || window.ControlType != ControlType.Window)
                throw new ArgumentException("Element is not a Window.");

            return window;
        }
    }
}
