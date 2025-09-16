# 🚀 UIAutomationTestKit

## 📖 Read this in other languages
- Russian: [README.md](README.md)

## 📋 Table of Contents
- [Project Overview](#-project-overview)
- [Key Benefits](#-key-benefits)
- [System Requirements](#-system-requirements)
- [Getting Started](#-getting-started)
  - [Installation](#installation)
  - [Create Your First Test](#create-your-first-test)
- [Architecture](#-architecture)
  - [Page Object Model (POM)](#page-object-model-pom)
- [Project Structure](#-project-structure)
- [Core Components](#-core-components)
  - [Controllers](#controllers)
  - [Locators](#locators)
  - [Extensions](#extensions)
- [Working with Tests](#-working-with-tests)
  - [Parameterized Tests](#parameterized-tests)
  - [JSON Test Data](#json-test-data)
- [Reporting and Logging](#-reporting-and-logging)
  - [HTML Reports](#html-reports)
  - [Logging Configuration](#logging-configuration)
- [Debugging](#-debugging)
  - [Wait Methods](#wait-methods)
  - [Debugging Tips](#debugging-tips)
- [Best Practices](#-best-practices)
- [Contributing](#-contributing)
  - [Code Style Guidelines](#code-style-guidelines)
- [Contact](#-contact)


## 📝 Project Overview

**UIAutomationTestKit** is a modern framework for automating tests of .NET desktop applications, built on top of FlaUI and NUnit. It implements the Page Object Model pattern and provides a convenient Fluent API for writing readable and maintainable UI tests.

Widely used by professionals — our framework is adopted by hundreds of testers and developers both in real projects and for learning. It has become a de facto standard for desktop automation thanks to its reliability, ease of use, and clear documentation.

### 🎯 Key Benefits
- 📌 Intuitive Fluent API
- 📌 Modular architecture
- 📌 Easy extensibility
- 📌 Built-in wait handling
- 📌 Detailed logging
- 📌 Parameterized tests support
- 📌 HTML report generation

## 💻 System Requirements

- Windows 10 or later
- .NET 9.0
- Visual Studio 2022 or later
- NuGet packages:
  - FlaUI.Core
  - FlaUI.UIA3
  - NUnit
  - NLog

## 🚦 Getting Started

### Installation
1. Clone the repository:
```bash
git clone https://github.com/AlekseyZabrodin2/UIAutomationTestKit.git
```

2. Restore dependencies via NuGet:
```bash
dotnet restore
```

### Create Your First Test
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

## 🏗 Architecture

### Page Object Model (POM)
The framework is based on the Page Object Model pattern, which provides:
- Separation of test logic from UI implementation details
- Code reuse
- Ease of maintenance

## 📂 Project Structure

```
UIAutomationTestKit/                  # Main application project
├── 📁 Views/                        # XAML views
│   └── MainWindow.xaml              # Main application window
├── 📁 ViewModels/                   # View models
├── App.xaml                         # App configuration
└── App.xaml.cs                      # App logic

UiAutoTests/                         # Automated tests project
├── 📁 Core/                         # Base classes and interfaces
├── 📁 Controllers/                  # Controllers for UI elements
├── 📁 ControllerAssertions/        # Assertions for controllers
├── 📁 Locators/                    # UI element locators
├── 📁 Extensions/                  # Extension methods
├── 📁 Helpers/                     # Helper classes
├── 📁 Services/                    # Data-related services
├── 📁 Clients/                     # External service clients
├── 📁 Tests/                       # Test scenarios
├── 📁 TestCasesData/              # Test case data
├── 📁 TestDataJson/               # JSON files with test data
└── NLog.config                     # Logging configuration
```

## 🔧 Core Components

### Controllers
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

### Locators
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

### Extensions
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
        _logger.Info($"Entered text: \"{text}\"");
    }
}
```

## 📋 Working with Tests

### Parameterized Tests
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

### JSON Test Data
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

## 📈 Reporting and Logging

### HTML Reports
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

### Logging Configuration
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
></nlog>
```

## 🔍 Debugging

### Wait Methods
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

### Debugging Tips
1. Use the `Pause()` method to slow down test execution:
```csharp
mainWindowController.Pause(1000); // 1 second pause
```

2. Add detailed logging:
```csharp
_logger.Info($"Entering {nameof(SetUserId)} with value: {inputText}");
```

## ✨ Best Practices

1. Always use the Fluent API to improve test readability
2. Add informative messages to assertions
3. Prefer waits over fixed delays
4. Group tests by functionality
5. Follow SOLID principles
6. Use logging for debugging

## 🤝 Contributing

We welcome contributions! To get started:

1. Fork the repository
2. Create a feature branch
3. Make changes and open a pull request

### Code Style Guidelines
- Use meaningful names for variables and methods
- Add comments for complex logic
- Follow SOLID principles
- Cover new code with tests
- Use logging for debugging

## 📧 Contact

📧 For any questions: [alekseyzabrodin2.0@gmail.com]


