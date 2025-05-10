using UiAutoTests.Core;
using UiAutoTests.Services;

namespace UiAutoTests
{

    public class TestsExample
    {

        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ITestClient _testClient;
        private IClientState _mainWindow;
        public string _testName;
        public string _testClass;
        public HtmlReport _reportCore = new();
        private TestsInitializeManager _initializeManager = new();


        public TestsExample()
        {
            _testClient = new AutomationTestClient("..\\..\\..\\..\\UIAutomationTestKit\\bin\\Debug\\net9.0-windows\\UIAutomationTestKit.exe");
        }



        [SetUp]
        public void Setup()
        {
            _testClass = GetType().Name;
            _testName = _initializeManager.CreateTestName();

            _logger.Trace($"\r\n=========================== New Test {_testName} ===========================");

            _mainWindow = _initializeManager.SetUpBeforeTest(_testName, _testClass, _reportCore, _testClient);
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
        //        if (_mainWindow is MainWindowState mainWindowState)
        //        {
        //            mainWindowState.SelectDepositItem(depositIndex);
        //            mainWindowState.SelectLeverages(leveragesIndex);
        //            mainWindowState.SelectCurrencies(currenciesIndex);
        //            mainWindowState.SelectAccountType(accountTypeIndex);

        //            var resultValidation = mainWindowState.ValidationDeposits();
        //            Assert.That(resultValidation, Is.True, $"ComboBox is Valid - [{resultValidation}]");

        //            var buttonIsEnabled = mainWindowState.CheckingCreateAccountButtonIsEnabled();
        //            Assert.That(buttonIsEnabled, Is.True, $"CreateAccount Button IsEnabled - [{buttonIsEnabled}]");

        //            mainWindowState.CreateAccounte();

        //            // Pause for demonstration
        //            mainWindowState.WaitingBetweenCommand(1000);

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
        public void Test1()
        {
            try
            {
                if (_mainWindow is MainWindowState mainWindowState)
                {
                    
                   // mainWindowState.SelectDepositItem(depositIndex);
                   // mainWindowState.SelectLeverages(leveragesIndex);
                   // mainWindowState.SelectCurrencies(currenciesIndex);
                   // mainWindowState.SelectAccountType(accountTypeIndex);

                   // var resultValidation = mainWindowState.ValidationDeposits();
                   // Assert.That(resultValidation, Is.True, $"ComboBox is Valid - [{resultValidation}]");

                   // var buttonIsEnabled = mainWindowState.CheckingCreateAccountButtonIsEnabled();
                   // Assert.That(buttonIsEnabled, Is.True, $"CreateAccount Button IsEnabled - [{buttonIsEnabled}]");

                   // mainWindowState.CreateAccounte();

                   // Pause for demonstration

                   //mainWindowState.WaitingBetweenCommand(1000);

                   _logger.Debug($"{_testName} Completed");
                    _reportCore.LogStatusPass(_testName + " Completed");
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"{_testName} Failed");
                _reportCore.LogStatusFail(exception, _testName + " Failed");
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
            _initializeManager.CleanupAfterTest(_testClient, _reportCore);
        }

    }
}
