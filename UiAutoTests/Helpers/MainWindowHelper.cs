using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using NLog;
using System.ComponentModel;
using UiAutoTests.Core;
using UiAutoTests.Extensions;
using UiAutoTests.Locators;

namespace UiAutoTests.Helpers
{
    public class MainWindowHelper
    {

        private readonly Window _window;
        private readonly ConditionFactory _conditionFactory;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();
        private MainWindowLocators _mainWindowLocators;


        public MainWindowHelper(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
            _mainWindowLocators = new(_window, _conditionFactory);
        }



        public void SetUserId(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.UserIdTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetLastName(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.UserLastNameTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetMiddleName(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.UserMiddleNameTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetFirstName(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.UserFirstNameTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }


        public void CheckedBirthdate()
        {
            _loggerHelper.LogEnteringTheMethod();

            var checkBox = _mainWindowLocators.BirthDateUserCheckBox;
            checkBox.CheckBoxChecked();
        }

        public void UncheckedBirthdate()
        {
            _loggerHelper.LogEnteringTheMethod();

            var checkBox = _mainWindowLocators.BirthDateUserCheckBox;
            checkBox.CheckBoxUnchecked();
        }

        public void SetBirthdateText(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var datePicker = _mainWindowLocators.UserBirthDateDatePicker;
            datePicker.Focus();
            datePicker.SelectedDate = DateTime.Parse(inputText);
        }

        public void SelectGender(int genderIndex)
        {
            _loggerHelper.LogEnteringTheMethod();

            var comboBox = _mainWindowLocators.GenderUserComboBox;
            comboBox.SelectItemByIndex(genderIndex);
        }

        public void SetAddressUser(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.AddressUserTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetPhoneUser(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.PhoneUserTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void SetInfoUser(string inputText)
        {
            _loggerHelper.LogEnteringTheMethod();

            var textBox = _mainWindowLocators.InfoUserTextBox;
            textBox.FocusTextBoxAndSetCursor();
            textBox.EnterText(inputText);
        }

        public void ClickCalendarPreviousButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var previousButton = _mainWindowLocators.PART_PreviousButton;
            previousButton.ClickButton();
        }

        public void ClickCalendarHeaderButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var headerButton = _mainWindowLocators.PART_HeaderButton;
            headerButton.ClickButton();

        }

        public void ClickCalendarNextButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var nextButton = _mainWindowLocators.PART_NextButton.AsButton();
            nextButton.ClickButton();
        }

        public void ClickCalendarDayButton(string buttonName)
        {
            _loggerHelper.LogEnteringTheMethod();

            var calendarDayButton = _mainWindowLocators.CalendarDayButtons;
            var button = calendarDayButton.FirstOrDefault(name => name.Name == buttonName).AsButton();
            button.ClickButton();
        }

        public RadioButton[] GetAllRadioButtons()
        {
            _loggerHelper.LogEnteringTheMethod();

            return new[]
            {
                _mainWindowLocators.RadioButtonPassport,
                _mainWindowLocators.RadioButtonDriverLicense,
                _mainWindowLocators.RadioButtonIdCard
            };
        }

        public void SelectRandomRadioButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var radioButtons = GetAllRadioButtons();

            var random = new Random();
            var result = random.Next(0, radioButtons.Length);
            var button = radioButtons[result];

            button.SelectRadioButton();
        }

        public void SelectRadioButtonByIndex(int index)
        {
            _loggerHelper.LogEnteringTheMethod();

            var radioButtons = GetAllRadioButtons();
            var button = radioButtons[index];

            button.SelectRadioButton();
        }

        public void SelectUserCountBySlider(int count)
        {
            _loggerHelper.LogEnteringTheMethod();

            var slider = _mainWindowLocators.SliderCount;
            slider.SetValue(count);
        }

        public void ClickCleanButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var cleanUpButton = _mainWindowLocators.CleanUpFieldsButton;
            cleanUpButton.ClickButton();
        }

        public void ClickRegistrationButton()
        {
            _loggerHelper.LogEnteringTheMethod();

            var regButton = _mainWindowLocators.RegistrationUserButton;
            regButton.ClickButton();
        }

        public void WaitUntilProgressBarIs(int prograssBarValue)
        {
            _loggerHelper.LogEnteringTheMethod();

            var progressBar = _mainWindowLocators.UserGenerationProgressBar;
            progressBar.WaitUntilValueIs(prograssBarValue);
        }

        public void WaitProgressBarWithToken(int expectedValue, CancellationToken cancellationToken = default)
        {
            _loggerHelper.LogEnteringTheMethod();

            while (GetProgressBarValue()  != expectedValue)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Thread.Sleep(100);
            }
        }

        public double GetProgressBarValue()
        {
            _loggerHelper.LogEnteringTheMethod();

            var progressBar = _mainWindowLocators.UserGenerationProgressBar;
            return progressBar.GetValue();
        }

        public bool IsRegistrationButtonEnabled()
        {
            _loggerHelper.LogEnteringTheMethod();

            var button = _mainWindowLocators.RegistrationUserButton;
            return button.IsButtonEnabled();
        }

        public string GetTextFromUserIdTextBox()
        {
            _loggerHelper.LogEnteringTheMethod();

            var userIdTextBox = _mainWindowLocators.UserIdTextBox;

            return userIdTextBox.GetText();
        }

        public void RegistrationSeveralUsers(int count)
        {
            _loggerHelper.LogEnteringTheMethod();

            SetValidDataInUserForm(2, count);
            ClickRegistrationButton();
            WaitUntilProgressBarIs(count);
        }

        public int GetRowCountInDataGrid()
        {
            _loggerHelper.LogEnteringTheMethod();

            var dataGrid = _mainWindowLocators.UsersCollectionDataGrid;
            var result = dataGrid.GetRowCount();

            return result;
        }


        public void Pause(int timeInSecond)
        {
            _loggerHelper.LogEnteringTheMethod();

            WaitExtensions.Pause(timeInSecond);
        }

        public void WaitUntilTextIsEmpty(int timeInSecond)
        {
            _loggerHelper.LogEnteringTheMethod();

            var userIdTextBox = _mainWindowLocators.UserIdTextBox;
            userIdTextBox.WaitUntilTextIsEmpty(timeInSecond);
        }

        public void SetValidDataInUserForm(int genderCount, int userCount)
        {
            _loggerHelper.LogEnteringTheMethod();

            var dayStringFormat = DateTime.Now.AddDays(-3).ToLongDateString();

            SetUserId("1");
            SetLastName("Ivanov");
            SetMiddleName("Ivan");
            SetFirstName("Ivanovich");
            CheckedBirthdate();
            SelectGender(genderCount);
            SetBirthdateText("25.12.1995");
            SetAddressUser("London, Baker Street 221B");
            SetPhoneUser("5465431");
            SetInfoUser("Second test case with different data");
            ClickCalendarDayButton(dayStringFormat);
            SelectRandomRadioButton();

            SelectUserCountBySlider(userCount);
        }

        public void EnsureClientStopped(ITestClient testClient, string clientName = "default")
        {
            _loggerHelper.LogEnteringTheMethod();

            try
            {
                if (testClient == null)
                {
                    _logger.Debug($"Client '{clientName}' is null - nothing to stop");
                    return;
                }

                testClient.Kill();
                _logger.Debug($"Client '{clientName}' stopped successfully");
            }
            catch (ObjectDisposedException)
            {
                _logger.Debug($"Client '{clientName}' already disposed");
            }
            catch (InvalidOperationException ex)
            {
                _logger.Debug(ex, $"Client '{clientName}' already stopped or in invalid state");
            }
            catch (Win32Exception ex)
            {
                _logger.Warn(ex, $"Win32 exception during client '{clientName}' shutdown");
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, $"Failed to stop client '{clientName}' safely");
            }
        }

        public void ExpandMenuItemById(string menuItemName)
        {
            _loggerHelper.LogEnteringTheMethod();

            var elements = _mainWindowLocators.FindAllById(menuItemName);
            var menuItem = elements.FirstOrDefault(p => p.AutomationId == menuItemName).AsMenuItem();

            menuItem.ExpandMenuItem();
        }

        public void ClickMenuItemById(string menuItemName)
        {
            _loggerHelper.LogEnteringTheMethod();

            var elements = _mainWindowLocators.FindAllById(menuItemName);
            var menuItem = elements.FirstOrDefault(p => p.AutomationId == menuItemName).AsMenuItem();

            menuItem.ClickMenuItem();
        }

        public Menu GetMainMenu()
        {
            _loggerHelper.LogEnteringTheMethod();

            return _mainWindowLocators.MainMenu;
        }

        public MenuItem GetMenuItemById(string menuItemName)
        {
            _loggerHelper.LogEnteringTheMethod();

            var element = _mainWindowLocators.FindAllById(menuItemName);
            var menuItem = element.FirstOrDefault(p => p.AutomationId == menuItemName).AsMenuItem();

            return menuItem;
        }

        public AutomationElement FindMessageBox()
        {
            _loggerHelper.LogEnteringTheMethod();

            return _mainWindowLocators.MessageBox;
        }

        public string GetMessageBoxText()
        {
            _loggerHelper.LogEnteringTheMethod();

            var messageBox = FindMessageBox();
            var text = messageBox.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text).And(cf.ByClassName("Static")));
            _logger.Debug($"Text from MessageBox is - [{text?.Name}]");

            return text.Name;
        }

        public void OpenAboutAppWindow()
        {
            _loggerHelper.LogEnteringTheMethod();

            ExpandMenuItemById("HelpMenuItem");
            ClickMenuItemById("AboutMenuItem");
        }

        public void CloseAboutAppWindow()
        {
            _loggerHelper.LogEnteringTheMethod();

            using (var automation = new UIA3Automation())
            {
                var aboutWindow = GetAboutAppWindow(automation);
                if (aboutWindow == null)
                {
                    _logger.Warn("Cannot close [About window] - not found");
                    return;
                }

                var aboutWindowLocators = new AboutAppWindowLocators(aboutWindow, _conditionFactory);
                var okButton = aboutWindowLocators.AboutAppOkButton;
                okButton.Invoke();
            }
        }


        /// <summary>
        /// Находит и возвращает окно "About" на рабочем столе Windows.
        /// 
        /// ПОЧЕМУ ИМЕННО ТАКОЙ ПОДХОД:
        /// 1. About окно - ЭТО НЕ ДОЧЕРНИЙ ЭЛЕМЕНТ главного окна приложения, 
        ///    а ОТДЕЛЬНОЕ НЕЗАВИСИМОЕ ОКНО верхнего уровня на рабочем столе.
        ///    Поэтому поиск должен вестись не в пределах главного окна (_window),
        ///    а на всем рабочем столе (desktop).
        /// 
        /// 2. Используем FindFirstChild(c => condition) вместо FindAllChildren() 
        ///    для ЭФФЕКТИВНОСТИ - метод остановится на первом же найденном совпадении,
        ///    не перебирая все элементы рабочего стола.
        /// 
        /// 3. Комбинируем условия через .And() для ТОЧНОСТИ ПОИСКА:
        ///    - ByControlType(ControlType.Window) - ищем именно окна, а не другие элементы
        ///    - ByAutomationId("AboutAppView") - находим конкретное окно по уникальному ID
        ///    
        /// 4. Используем null-conditional оператор (?.) для БЕЗОПАСНОСТИ -
        ///    если окно не найдено, возвращаем null вместо исключения.
        /// 
        /// 5. Получаем desktop через automation.GetDesktop() - это КОРНЕВОЙ ЭЛЕМЕНТ,
        ///    содержащий все открытые окна Windows.
        /// </summary>
        /// <param name="automation">Экземпляр UIA3Automation для доступа к automation tree</param>
        /// <returns>Найденное окно About или null если окно не найдено</returns>
        public Window GetAboutAppWindow(UIA3Automation automation)
        {
            _loggerHelper.LogEnteringTheMethod();

            var desktop = automation.GetDesktop();
            var aboutWindow = desktop.FindFirstChild(cf => cf.ByControlType(ControlType.Window).And(cf.ByAutomationId("AboutAppView")));

            return aboutWindow?.AsWindow();
        }

    }
}