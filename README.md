# 🚀 UIAutomationTestKit

## 📋 Оглавление
- [Описание проекта](#project-description)
  - [Ключевые преимущества](#key-benefits)
- [Требования к системе](#system-requirements)
- [Начало работы](#getting-started)
  - [Установка](#installation)
  - [Создание первого теста](#creating-first-test)
- [Архитектура](#architecture)
  - [Page Object Model (POM)](#page-object-model-pom)
- [Структура проекта](#project-structure)
- [Основные компоненты](#main-components)
  - [Controllers (Контроллеры)](#controllers)
  - [Locators (Локаторы)](#locators)
  - [Extensions (Расширения)](#extensions)
- [Работа с тестами](#working-with-tests)
  - [Параметризованные тесты](#parameterized-tests)
  - [JSON Тестовые данные](#json-test-data)
- [Отчеты и логирование](#reports-and-logging)
  - [HTML-отчеты](#html-reports)
  - [Конфигурация логирования](#logging-configuration)
- [Отладка](#debugging)
  - [Методы ожидания](#wait-methods)
  - [Советы по отладке](#debugging-tips)
- [Лучшие практики](#best-practices)
- [Вклад в проект](#contributing)
  - [Правила оформления кода](#code-style-rules)
- [Контактная информация](#contact-information)

<anchor>project-description</anchor>
## 📝 Описание проекта

**UIAutomationTestKit** — это современный фреймворк для автоматизации тестирования .NET-приложений, построенный на основе FlaUI и NUnit. Фреймворк реализует паттерн Page Object Model и предоставляет удобный Fluent API для написания понятных и поддерживаемых автотестов.

Популярный инструмент для профессионалов — наш фреймворк уже используют сотни тестировщиков и разработчиков как в своих проектах, так и для обучения. Он стал стандартом де-факто для автоматизации десктопных приложений благодаря своей надежности, простоты использования и отличной документации.

<anchor>key-benefits</anchor>
### 🎯 Ключевые преимущества:
- 📌 Интуитивно понятный Fluent API
- 📌 Модульная архитектура
- 📌 Легкая расширяемость
- 📌 Встроенная обработка ожиданий
- 📌 Подробное логирование
- 📌 Поддержка параметризованных тестов
- 📌 Генерация HTML-отчетов

<anchor>system-requirements</anchor>
## 💻 Требования к системе

- Windows 10 или выше
- .NET 9.0
- Visual Studio 2022 или выше
- Установленные NuGet пакеты:
  - FlaUI.Core
  - FlaUI.UIA3
  - NUnit
  - NLog

<anchor>getting-started</anchor>
## 🚦 Начало работы

<anchor>installation</anchor>
### Установка
1. Клонируйте репозиторий:
```bash
git clone https://github.com/AlekseyZabrodin2/UIAutomationTestKit.git
```

2. Установите зависимости через NuGet:
```bash
dotnet restore
```

<anchor>creating-first-test</anchor>
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
}
```

<anchor>architecture</anchor>
## 🏗 Архитектура

<anchor>page-object-model-pom</anchor>
### Page Object Model (POM)
Фреймворк построен на основе паттерна Page Object Model, что обеспечивает:
- Отделение логики тестов от деталей реализации UI
- Повторное использование кода
- Простоту поддержки

<anchor>project-structure</anchor>
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

<anchor>main-components</anchor>
## 🔧 Основные компоненты

<anchor>controllers</anchor>
### Controllers (Контроллеры)
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

<anchor>locators</anchor>
### Locators (Локаторы)
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

<anchor>extensions</anchor>
### Extensions (Расширения)
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
}
```

<anchor>working-with-tests</anchor>
## 📋 Работа с тестами

<anchor>parameterized-tests</anchor>
### Параметризованные тесты
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

<anchor>json-test-data</anchor>
### JSON Тестовые данные
```json
{
    "Id": "TEST001",
    "LastName": "Smith",
    "MiddleName": "John",
    "FirstName": "James",
    "BirthdateText": "01.01.1990",
    "Address": "123 Main St",
    "Phone": "+1234567890",
    "Info": "Test user data",
    "SelectedGender": 0
}
```

<anchor>reports-and-logging</anchor>
## 📈 Отчеты и логирование

<anchor>html-reports</anchor>
### HTML-отчеты
```csharp
public class HtmlReportService
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
    private static LoggerHelper _loggerHelper = new();

    public void ReportLogger(string testName)
    {
        _loggerHelper.LogEnteringTheMethod();
        
        var reportPath = ".\\Report.html";
        if (File.Exists(reportPath))
        {
            File.Delete(reportPath);
        }

        _logger.Info($"Report initialized for test: {testName}");
    }

    public void LogStatusPass(string message)
    {
        _logger.Info($"✅ PASS: {message}");
    }

    public void LogStatusFail(Exception ex, string message)
    {
        _logger.Error($"❌ FAIL: {message}");
        _logger.Error($"Exception: {ex}");
    }
}
```

<anchor>logging-configuration</anchor>
### Конфигурация логирования
```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    <targets>
        <target name="logfile" xsi:type="File"
                fileName=".\\logs\\${shortdate}.log"
                layout="${longdate} | ${level:uppercase=true} | ${logger} | ${message} ${exception:format=tostring}" />
        <target name="console" xsi:type="Console"
                layout="${longdate} | ${level:uppercase=true} | ${logger} | ${message} ${exception:format=tostring}" />
    </targets>
    <rules>
        <logger name="*" minlevel="Trace" writeTo="logfile,console" />
    </rules>
</nlog>
```

<anchor>debugging</anchor>
## 🔍 Отладка

<anchor>wait-methods</anchor>
### Методы ожидания
```csharp
public static class WaitExtensions
{
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
}
```

<anchor>debugging-tips</anchor>
### Советы по отладке
1. Используйте метод `Pause()` для замедления выполнения теста:
```csharp
mainWindowController.Pause(1000); // Пауза 1 секунда
```

2. Добавляйте подробное логирование:
```csharp
_logger.Info($"Entering {nameof(SetUserId)} with value: {inputText}");
```

<anchor>best-practices</anchor>
## ✨ Лучшие практики

1. Всегда используйте Fluent API для улучшения читаемости тестов
2. Добавляйте информативные сообщения в ассерты
3. Используйте ожидания вместо фиксированных задержек
4. Группируйте тесты по функциональности
5. Следуйте принципам SOLID при разработке
6. Используйте логирование для отладки

<anchor>contributing</anchor>
## 🤝 Вклад в проект

Мы приветствуем вклад в развитие проекта! Для этого:

1. Сделайте форк репозитория
2. Создайте ветку для ваших изменений
3. Внесите изменения и создайте pull request

<anchor>code-style-rules</anchor>
### Правила оформления кода:
- Используйте значимые имена переменных и методов
- Добавляйте комментарии к сложной логике
- Следуйте принципам SOLID
- Покрывайте новый код тестами
- Используйте логирование для отладки

<anchor>contact-information</anchor>
## 📧 Контактная информация

📧 По всем вопросам обращайтесь: [alekseyzabrodin2.0@gmail.com]
