# AutomationTestKit

## Описание проекта

**AutomationTestKit** — это пример модульного фреймворка для автоматизации тестирования .NET-приложений с использованием FlaUI и NUnit. Проект построен по принципам гибкости, читаемости и масштабируемости: все действия над UI оформлены в виде Page Object-классов, логика работы вынесена в расширения, используется Fluent API-стиль, что позволяет писать тесты как последовательность читаемых шагов.

## Основные технологии

- [.NET9]
- [FlaUI]
- [NUnit]
- [NLog]
- Page Object Model (POM)
- Fluent-style для шагов

## Структура проекта

```
AutomationTestKit/
│
├── 📁 Controllers/                ← логика взаимодействия с UI
│   ├── MainWindowController.cs
│   └── NextWindowController.cs
│
├── 📁 Pages/                     ← Page Object слои (локаторы), если экранов много
│   ├── RegistrationPageLocators.cs
│   ├── Locators.cs
│   └── ...
│
├── 📁 Extensions/                ← методы расширения для элементов
│   ├── AutomationElementExtensions.cs
│   └── UIElementHelpers.cs
│
├── 📁 Asserts/                   ← Fluent-ассерты
│   ├── MainWindowAssert.cs
│   └── CommonAsserts.cs
│
├── 📁 Helpers/                   ← общие утилиты (ввод, логгер и т.п.)
│   ├── KeyboardHelper.cs
│   └── WaitHelper.cs
│
├── 📁 TestBase/                  ← базовые классы, setup/teardown
│   ├── BaseUiTest.cs
│   └── AppManager.cs
│
├── 📁 Tests/
│   ├── ExampleTests.cs
│   └── SmokeTests.cs
│
├── 📁 Resources/                 ← константы
│   └── Constants.cs
│
└── App.config / appsettings.json ← настройки путей, таймаутов



📚 Структура AutomationTestKit

📁 Controllers/

Что тут:
Классы для работы с экранами: находим элементы, кликаем, заполняем поля.

Пример MainWindowController.cs:

public class MainWindowController
{
    private readonly Window _window;

    public MainWindowController(Window window)
    {
        _window = window;
    }

    public MainWindowController ClickAddButton()
    {
        var addButton = _window.FindFirstDescendant(cf => cf.ByAutomationId("AddButton_AId")).AsButton();
        addButton.Click();
        return this;
    }

    public string GetTitle() => _window.Title;
}

Объяснение:

    Находит кнопку.

    Кликает.

    Возвращает себя для Fluent-стиля.

📁 Pages/

Что тут:
Чистые Page Object — только структура элементов. Никакой логики.

Пример RegistrationPage.cs:

public static class RegistrationPage
{
    public const string AddButtonId = "AddButton_AId";
    public const string SearchTextBoxId = "SearchBox_AId";
}

Объяснение:

    Константы локаторов.

    Чтобы локаторы были в одном месте.

📁 Extensions/

Что тут:
Методы расширения для UI-элементов — чтобы код был читаемым.

Пример AutomationElementExtensions.cs:

public static class AutomationElementExtensions
{
    public static void ClickElement(this AutomationElement element)
    {
        element.AsButton()?.Invoke();
    }

    public static void EnterText(this AutomationElement element, string text)
    {
        element.AsTextBox()?.Enter(text);
    }
}

Объяснение:

    myButton.ClickElement(); вместо 3 строк кода.

📁 Asserts/

Что тут:
Проверки в стиле Fluent — красиво читается.

Пример MainWindowAssert.cs:

public static class MainWindowAssert
{
    public static void ShouldBeVisible(this MainWindowController controller)
    {
        Assert.That(controller.IsWindowDisplayed, Is.True, "Add Main window is not visible");
    }
}

Объяснение:

    Тесты: controller.ShouldBeVisible();

    Упрощает код тестов.

📁 Helpers/

Что тут:
Маленькие универсальные помощники.

Пример KeyboardHelper.cs:

public static class KeyboardHelper
{
    public static void TypeText(string text)
    {
        Keyboard.Type(text);
    }
}

Пример WaitHelper.cs:

public static class WaitHelper
{
    public static void WaitForSeconds(double seconds)
    {
        Thread.Sleep(TimeSpan.FromSeconds(seconds));
    }
}

📁 TestBase/

Что тут:
Базовые классы для запуска и закрытия приложения.

Пример BaseUiTest.cs:

[TestFixture]
public abstract class BaseUiTest
{
    protected Application App;
    protected Window MainWindow;

    [SetUp]
    public void SetUp()
    {
        App = Application.Launch("path_to_app.exe");
        MainWindow = App.GetMainWindow();
    }

    [TearDown]
    public void TearDown()
    {
        App.Close();
    }
}

📁 Tests/

Что тут:
Реальные тесты!

Пример ExampleTests.cs:

[TestFixture]
public class ExampleTests : BaseUiTest
{
    [Test]
    public void Should_Search_And_Find_Element()
    {
        var controller = new MainWindowController(MainWindow)
                             .ClickAddButton()
                             .Search("Sample text");

        controller.ShouldBeVisible();
        var result = controller.GetFirstSearchResult();
        Assert.That(result.Name, Is.EqualTo("Sample text"));
    }
}

📁 Resources/

📄 App.config / appsettings.json

Что тут:
Настройки таймаутов, путей до приложений и т.д.

Пример appsettings.json:

{
  "ApplicationPath": "path_to_app.exe",
  "DefaultTimeoutSeconds": 10
}

И читаем потом:

var path = ConfigurationManager.AppSettings["ApplicationPath"];

✨ В итоге
Часть                | За что отвечает
Controller           | Логика работы с UI
Page                 | Где хранятся локаторы
Extensions           | Удобные методы для элементов
Asserts              | Красивая проверка состояния
Helpers              | Мелкие утилиты
TestBase             | Setup/Teardown окружения
Tests                | Сами тесты
Resources            | Константы, настройки

```

## Возможности

- Автоматическое тестирование пользовательского интерфейса (UI)
- Интерактивное управление оборудованием через TCP
- Поддержка пошаговой верификации состояния UI
- Логирование действий и ошибок в файл
- Параметризованные тесты (в т.ч. с `Values`, `TestCase`)
- Инициализация окружения через `SetUpFixture`, `OneTimeSetUp`, `SetUp` и `TearDown`


