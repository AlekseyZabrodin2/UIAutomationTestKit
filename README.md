# 🚀 UIAutomationTestKit

## 📋 Оглавление
- [Описание проекта](#описание-проекта)
- [Технологии](#технологии)
- [Архитектура](#архитектура)
- [Начало работы](#начало-работы)
- [Структура проекта](#структура-проекта)
- [Компоненты](#компоненты)
- [Лучшие практики](#лучшие-практики)
- [Вклад в проект](#вклад-в-проект)

## 📝 Описание проекта

**UIAutomationTestKit** — это современный фреймворк для автоматизации тестирования .NET-приложений, построенный на основе FlaUI и NUnit. Фреймворк реализует паттерн Page Object Model и предоставляет удобный Fluent API для написания понятных и поддерживаемых автотестов.

### 🎯 Ключевые преимущества:
- 📌 Интуитивно понятный Fluent API
- 📌 Модульная архитектура
- 📌 Легкая расширяемость
- 📌 Встроенная обработка ожиданий
- 📌 Подробное логирование
- 📌 Поддержка параметризованных тестов
- 📌 Генерация HTML-отчетов

## 🛠 Технологии

- **.NET 9** - Современная платформа разработки
- **FlaUI** - Мощный фреймворк для автоматизации UI
- **NUnit** - Фреймворк модульного тестирования
- **NLog** - Система логирования

## 🏗 Архитектура

### Page Object Model (POM)
Фреймворк построен на основе паттерна Page Object Model, что обеспечивает:
- Отделение логики тестов от деталей реализации UI
- Повторное использование кода
- Простоту поддержки

### Основные компоненты:

#### 1. Controllers (Контроллеры)
Отвечают за взаимодействие с UI элементами.

```csharp
public class MainWindowController : IClientState
{
    private readonly Window _window;
    private readonly ConditionFactory _conditionFactory;
    private MainWindowLocators _mainWindowStateLocators;
    private LoggerHelper _loggerHelper = new();
    private static MainWindowHelper _mainWindowHelper;
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public string Name { get; } = "MainWindowState";

    public MainWindowController(Window window, ConditionFactory conditionFactory)
    {
        _window = window;
        _conditionFactory = conditionFactory;
        _mainWindowStateLocators = new(_window, _conditionFactory);
        _mainWindowHelper = new(_window, _conditionFactory);
    }

    public MainWindowController SetUserId(string inputText)
    {
        _mainWindowHelper.SetUserId(inputText);
        return this;
    }

    public MainWindowController ClickRegistrationButton()
    {
        _mainWindowHelper.ClickRegistrationButton();
        return this;
    }
}
```

#### 2. Locators (Локаторы)
Хранят информацию о расположении элементов UI.

```csharp
internal class MainWindowLocators
{
    private readonly Window _window;
    private readonly ConditionFactory _conditionFactory;

    private AutomationElement Find(string automationId) =>
        _window.FindFirstDescendant(_conditionFactory.ByAutomationId(automationId))
        ?? throw new ElementNotAvailableException($"Element with AutomationId - [{automationId}] not found");

    public TextBox UserIdTextBox => Find("UserIdTextBox").AsTextBox();
    public TextBox UserLastNameTextBox => Find("UserLastNameTextBox").AsTextBox();
    public TextBox UserMiddleNameTextBox => Find("UserMiddleNameTextBox").AsTextBox();        
    public TextBox UserFirstNameTextBox => Find("UserFirstNameTextBox").AsTextBox();
    public Button RegistrationUserButton => Find("RegistrationUserButton").AsButton();
    public CheckBox BirthDateUserCheckBox => Find("BirthDateUserCheckBox").AsCheckBox();
    public ComboBox GenderUserComboBox => Find("GenderUserComboBox").AsComboBox();
}
```

#### 3. Extensions (Расширения)
Добавляют удобные методы для работы с UI элементами.

```csharp
public static class TextBoxExtensions
{
    private static LoggerHelper _loggerHelper = new();
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

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
}
```

## 🚦 Начало работы

### Установка
1. Клонируйте репозиторий:
```bash
git clone https://github.com/your-repo/UIAutomationTestKit.git
```

2. Установите зависимости через NuGet:
```bash
dotnet restore
```

### Создание первого теста

```csharp
[Test]
public void Test02_After()
{
    try
    {
        if (_mainWindow is MainWindowController mainWindowControlle)
        {
            var inputText = "test Id";

            mainWindowControlle
                .SetUserId(inputText)
                .Pause(500)
                .AssertUserIdEquals(inputText,$"Expected Text to be [{inputText}]")
                .ClickCleanButton()
                .WaitUntilTextIsEmpty(500)
                .AssertUserIdIsEmpty("Expected TextBox to be Empty")
                .Pause(500);

            _loggerHelper.LogCompletedResult(_testName, _reportService);
        }
    }
    catch (Exception exception)
    {
        _loggerHelper.LogFailedResult(_testName, exception, _reportService);
        throw;
    }
    finally
    {
        _testClient.Kill();
    }
}
```

## 📂 Структура проекта

```
UIAutomationTestKit/                  # Основной проект приложения
├── 📁 Views/                        # Представления XAML
│   └── MainWindow.xaml              # Главное окно приложения
├── 📁 ViewModels/                   # Модели представлений
├── App.xaml                         # Конфигурация приложения
└── App.xaml.cs                      # Логика приложения

UiAutoTests/                         # Проект автоматизированных тестов
├── 📁 Core/                         # Базовые классы и интерфейсы
├── 📁 Controllers/                  # Контроллеры для работы с UI элементами
├── 📁 ControllerAssertions/        # Проверки для контроллеров
├── 📁 Locators/                    # Локаторы UI элементов
├── 📁 Extensions/                  # Методы расширения
├── 📁 Helpers/                     # Вспомогательные классы
├── 📁 Services/                    # Сервисы для работы с данными
├── 📁 Clients/                     # Клиенты для внешних сервисов
├── 📁 Tests/                       # Тестовые сценарии
├── 📁 TestCasesData/              # Данные для тестовых случаев
├── 📁 TestDataJson/               # JSON файлы с тестовыми данными
└── NLog.config                     # Конфигурация логирования
```

## ✨ Лучшие практики

### 1. Параметризованные тесты
```csharp
[TestCase("001", "Smith", "James", "John")]
[TestCase("002", "Johnson", "Lee", "Michael")]
[TestCase("003", "Williams", "Anne", "Emily")]
[Test]
public void Test04_WithParametersInTestCase(string id, string lastName, 
    string middleName, string firstName)
{
    try
    {
        if (_mainWindow is MainWindowController mainWindowControlle)
        {
            mainWindowControlle
                .SetUserId(id)
                .SetLastName(lastName)
                .SetMiddleName(middleName)
                .SetFirstName(firstName)
                .CheckedBirthdate()
                .AssertUserIdEquals(id, $"Expected Text to be [{id}]")
                .ClickCleanButton()
                .WaitUntilTextIsEmpty(500)
                .AssertUserIdIsEmpty("Expected TextBox to be Empty");

            _loggerHelper.LogCompletedResult(_testName, _reportService);
        }
    }
    catch (Exception exception)
    {
        _loggerHelper.LogFailedResult(_testName, exception, _reportService);
        throw;
    }
}
```

### 2. Обработка ожиданий
```csharp
public static class WaitExtensions
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public static bool WaitUntilEnabled(this AutomationElement element, int timeoutInSeconds)
    {
        return Retry.WhileFalse(() => element?.IsEnabled ?? false, 
            TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
    }

    public static bool WaitUntilTextIsEmpty(this TextBox element, int timeoutInSeconds)
    {
        return Retry.WhileFalse(
            () => element?.Text == string.Empty,
            TimeSpan.FromMilliseconds(timeoutInSeconds)).Success;
    }

    public static void EnterTextWhenReady(this TextBox textBox, string text, int timeoutInSeconds)
    {
        if (textBox.WaitUntilEnabled(timeoutInSeconds))
        {
            textBox.ClearTextWithKeyboard();
            textBox.Enter(text);
        }
        else
        {
            throw new TimeoutException("TextBox was not ready for input.");
        }
    }
}
```

### 3. Инициализация тестов
```csharp
[SetUpFixture]
public class AssemblyInitializeTests
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
    private static LoggerHelper _loggerHelper = new();
    public static HtmlReportService _reportService;

    [OneTimeSetUp]
    public void BeforeTestSuites()
    {
        _logger.Trace($"\r\n=========================== New Test Suite start  ===========================");
        _loggerHelper.LogEnteringTheMethod();

        _reportService = new();
        _reportService.ReportLogger("UI Test");

        _oldNameFullPath = ".\\Report.html";
        _newNameFullPath = ".\\logs\\ReportResults\\TestReport" + 
            DateTime.Now.ToString("_dd.MM.yyyy_HH.mm.ss") + ".html";

        var directoryPath = Path.GetDirectoryName(_newNameFullPath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        _reportService.ReplaceCssStyleDir();
    }
}
```

## 🤝 Вклад в проект

Мы приветствуем вклад в развитие проекта! Для этого:

1. Сделайте форк репозитория
2. Создайте ветку для ваших изменений
3. Внесите изменения и создайте pull request

### Правила оформления кода:
- Используйте значимые имена переменных и методов
- Добавляйте комментарии к сложной логике
- Следуйте принципам SOLID
- Покрывайте новый код тестами
- Используйте логирование для отладки

---

📧 По всем вопросам обращайтесь: [alekseyzabrodin2.0@gmail.com]
