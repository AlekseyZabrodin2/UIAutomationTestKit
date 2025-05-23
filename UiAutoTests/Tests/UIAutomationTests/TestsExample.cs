using UiAutoTests.Clients;
using UiAutoTests.ControllerAssertions;
using UiAutoTests.Controllers;
using UiAutoTests.Core;
using UiAutoTests.Helpers;
using UiAutoTests.Services;
using UiAutoTests.TestCasesData;

namespace UiAutoTests.Tests.UIAutomationTests
{

    public class TestsExample
    {

        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ITestClient _testClient;
        private IClientState _mainWindow;
        public string _testName;
        public string _testClass;
        private LoggerHelper _loggerHelper = new();
        public HtmlReportService _reportService = new();
        private TestsInitializeService _initializeService = new();


        public TestsExample()
        {
            _testClient = new AutomationTestClient("..\\..\\..\\..\\UIAutomationTestKit\\bin\\Debug\\net9.0-windows\\UIAutomationTestKit.exe");
        }



        [SetUp]
        public void Setup()
        {
            _testClass = GetType().Name;
            _testName = _initializeService.GetTestMethodName();
            _mainWindow = _initializeService.StartClientWithReportInitialization(_testName, _testClass, _reportService, _testClient);
        }



        //[TestCase(0, 0, 0, 0)]
        //[TestCase(1, 1, 1, 1)]
        //[TestCase(2, 3, 2, 0)]
        //[TestCase(3, 4, 0, 1)]
        //[TestCase(4, 5, 1, 0)]
        //[TestCase(5, 6, 2, 1)]
        //[TestCase("test string", 3, 2, 1)]
        //[TestCase(null, 3, 2, 1)]
        //[TestCase("", 3, 2, 1)]
        //[TestCase("99999999999", 3, 2, 1)]
        //[TestCase("5555", 3, 2, 1)]
        //[Test]
        //public void Test_RegistrationAccountWithParams(object? depositIndex, int leveragesIndex, int currenciesIndex, int accountTypeIndex)
        //{
        //    try
        //    {
        //        if (_mainWindow is mainWindowControlle mainWindowControlle)
        //        {
        //            mainWindowControlle.SelectDepositItem(depositIndex);
        //            mainWindowControlle.SelectLeverages(leveragesIndex);
        //            mainWindowControlle.SelectCurrencies(currenciesIndex);
        //            mainWindowControlle.SelectAccountType(accountTypeIndex);

        //            var resultValidation = mainWindowControlle.ValidationDeposits();
        //            Assert.That(resultValidation, Is.True, $"ComboBox is Valid - [{resultValidation}]");

        //            var buttonIsEnabled = mainWindowControlle.CheckingCreateAccountButtonIsEnabled();
        //            Assert.That(buttonIsEnabled, Is.True, $"CreateAccount Button IsEnabled - [{buttonIsEnabled}]");

        //            mainWindowControlle.CreateAccounte();

        //            // Pause for demonstration
        //            mainWindowControlle.WaitingBetweenCommand(1000);

        //            _logger.Debug($"{_testName} Completed");
        //            _reportCore.LogStatusPass(_testName + " Completed");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.Error(exception, $"{_testName} Failed");
        //        _reportCore.LogStatusFail(exception, _testName + " Failed");
        //        throw;
        //    }
        //    finally
        //    {
        //        _testClient.Kill();
        //    }
        //}



        [Test]
        public void Test01_Before()
        {
            try
            {
                if (_mainWindow is MainWindowController mainWindowControlle)
                {
                    var inputText = "test Id";

                    mainWindowControlle.SetUserId(inputText);
                    mainWindowControlle.Pause(500);

                    var actual = mainWindowControlle.GetUserIdText();
                    Assert.That(actual, Is.EqualTo(inputText), "Text is not equal");

                    mainWindowControlle.ClickCleanButton();

                    mainWindowControlle.Pause(1000);

                    var resultTest = mainWindowControlle.GetUserIdText();
                    Assert.That(resultTest, Is.Empty, "Text is not empty");

                    // Pause for demonstration
                    mainWindowControlle.Pause(1000);

                    _logger.Debug($"{_testName} Completed");
                    _reportService.LogStatusPass(_testName + " Completed");
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{_testName} Failed");
                _reportService.LogStatusFail(exception, _testName + " Failed");
                throw;
            }
            finally
            {
                _testClient.Kill();
            }
        }

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

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationFieldCases))]
        [Test]
        public void Test03_ParametersFromProperties(RegistrationCaseDto registrDto)
        {
            try
            {
                if (_mainWindow is MainWindowController mainWindowControlle)
                {
                    mainWindowControlle
                        .SetUserId(registrDto.Id)
                        .SetLastName(registrDto.LastName)
                        .SetMiddleName(registrDto.MiddleName)
                        .SetFirstName(registrDto.FirstName)

                        .CheckedBirthdate()

                        .AssertUserIdEquals(registrDto.Id, $"Expected Text to be [{registrDto.Id}]")

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

        [TestCase("001", "Smith", "James", "John")]
        [TestCase("002", "Johnson", "Lee", "Michael")]
        [TestCase("003", "Williams", "Anne", "Emily")]
        [TestCase("004", "Brown", "Marie", "Sophia")]
        [TestCase("005", "Davis", "Alan", "Robert")]
        [Test]
        public void Test04_WithParametersInTestCase(string id, string lastName, string middleName, string firstName)
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

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationCases))]
        [Test]
        public void Test05_ParametersFromClass(RegistrationCaseDto registrDto)
        {
            try
            {
                if (_mainWindow is MainWindowController mainWindowControlle)
                {
                    mainWindowControlle
                        .SetUserId(registrDto.Id)
                        .SetLastName(registrDto.LastName)
                        .SetMiddleName(registrDto.MiddleName)
                        .SetFirstName(registrDto.FirstName)

                        .CheckedBirthdate()
                        .SetBirthdateText(registrDto.BirthdateText)

                        .SetAdressUser(registrDto.Address)
                        .SetPhoneUser(registrDto.Phone)
                        .SetInfoUser(registrDto.Info)

                        .SelectGender(registrDto.SelectedGender)

                        .AssertUserIdEquals(registrDto.Id, $"Expected Text to be [{registrDto.Id}]")

                        .AssertIsRegistrationButtonEnabled()

                        .ClickRegistrationButton()
                        .Pause(1000);

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

        [TestCaseSource(typeof(MainWindowTestCases), nameof(MainWindowTestCases.ValidRegistrationCasesFromJson))]
        [Test]
        public void Test06_ParametersFromJson(RegistrationCaseFromJson dataFromJson)
        {
            try
            {
                if (_mainWindow is MainWindowController mainWindowControlle)
                {
                    mainWindowControlle
                        .SetUserId(dataFromJson.Id)
                        .SetLastName(dataFromJson.LastName)
                        .SetMiddleName(dataFromJson.MiddleName)
                        .SetFirstName(dataFromJson.FirstName)

                        .CheckedBirthdate()
                        .SetBirthdateText(dataFromJson.BirthdateText)

                        .SetAdressUser(dataFromJson.Address)
                        .SetPhoneUser(dataFromJson.Phone)
                        .SetInfoUser(dataFromJson.Info)

                        .SelectGender(dataFromJson.SelectedGender)

                        .AssertUserIdEquals(dataFromJson.Id, $"Expected Text to be [{dataFromJson.Id}]")

                        .AssertIsRegistrationButtonEnabled()

                        .ClickRegistrationButton()
                        .Pause(1000);

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




        [TearDown]
        public void AfterTest()
        {
            _initializeService.DisposeClientAndReportResults(_testClient, _reportService);
        }

    }
}
