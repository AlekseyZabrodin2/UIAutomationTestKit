using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using CommunityToolkit.Mvvm.ComponentModel;
using NLog;
using NUnit.Framework.Interfaces;
using UiAutoTests.Core;
using UiAutoTests.Helpers;

namespace UiAutoTests.Services
{
    public partial class HtmlReportService : ObservableObject, IReporter
    {
        public static ExtentSparkReporter _sparkReporter;
        public static TestContext _testContext;
        public static ExtentReports _report;
        public static ExtentTest _parentTest;
        public static ExtentTest _childTest;
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private LoggerHelper _loggerHelper = new();

        public string reportPath = ".\\Report.html";

        [ObservableProperty]
        private partial DateTime TimeTestStart {  get; set; }

        [ObservableProperty]
        private partial DateTime TimeTestStop { get; set; }



        public void ReportLogger(string testCaseName)
        {
            _loggerHelper.LogEnteringTheMethod();

            _sparkReporter = new ExtentSparkReporter(reportPath);

            var fileDire = Path.GetDirectoryName(reportPath);
            _logger.Trace($"CreateDirectory {fileDire}");
            if (!Directory.Exists(fileDire))
            {
                Directory.CreateDirectory(fileDire!);
            }

            _sparkReporter.Config.DocumentTitle = "BagVisionLC AutoTest Results Report";
            _sparkReporter.Config.ReportName = "Regression Testing";
            _sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
            _sparkReporter.Config.OfflineMode = true;
            _sparkReporter.Config.TimelineEnabled = false;
            _sparkReporter.Config.TimeStampFormat = "HH:mm:ss";

            _report = new ExtentReports();
            _report.AttachReporter(_sparkReporter);

            _report.AddSystemInfo("Machine Name", Environment.MachineName);
            _report.AddSystemInfo("User Domain-Name", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            _report.AddSystemInfo("OS", Environment.OSVersion.VersionString);

            TimeTestStart = DateTime.Now;
            _report.AddSystemInfo("Start Time Project", TimeTestStart.ToString());

            _logger.Trace("Exit from ReportLogger\r\n");

        }


        public void InitializeTests(string testContext, string tag)
        {
            CreateTest(testContext);
            CreateTags(tag);
        }


        public void CreateTest(string testContext)
        {
            _loggerHelper.LogEnteringTheMethod();

            _parentTest = _report.CreateTest(testContext);

            _parentTest.Log(Status.Info, $"Test Start - [ {testContext} ]");

            _parentTest.Log(Status.Info, "Start Test Time");
        }


        public void CreateNode(string node)
        {
            _parentTest.CreateNode(node);
        }


        public void CreateTags(string tags)
        {
            _parentTest.AssignCategory(tags);
        }

        public void LogErrorMessage(string message)
        {
            _parentTest.Log(Status.Warning, message);
            _parentTest.AssignCategory("Connection error");
        }


        public void LogStatusPass(string testName)
        {
            _loggerHelper.LogEnteringTheMethod();

            //_logger.Debug($"{testName} - [Completed]");

            string pass = "Pass";
            _parentTest.Log(Status.Pass, MarkupHelper.CreateLabel(pass.ToUpperInvariant(), ExtentColor.Green));

            _childTest = _parentTest.CreateNode("Status Pass Node");

            _childTest.Log(Status.Pass, testName);
        }


        public void LogStatusFail(Exception exception, string testName)
        {
            _loggerHelper.LogEnteringTheMethod();

            //_logger.Error(exception, $"{testName} - [Failed], with Exeption:");

            string faild = "Failed";
            _parentTest.Log(Status.Fail, MarkupHelper.CreateLabel(faild.ToUpperInvariant(), ExtentColor.Red));

            _parentTest.Log(Status.Error, $"Failed Exception: - [ <b>{exception.Message}</b> ]");

            _childTest = _parentTest.CreateNode("Exception Details");

            _childTest.Log(Status.Fail, testName);

            _childTest.Warning((Status.Warning, exception).ToString());

            _childTest.Log(Status.Warning, exception);
        }

        public void GetTestsStatus()
        {
            _loggerHelper.LogEnteringTheMethod();

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Passed:
                    logstatus = Status.Pass;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Error;
                    break;
                case TestStatus.Warning:
                    logstatus = Status.Warning;
                    break;
                default:
                    logstatus = Status.Error;
                    break;
            }

            _parentTest.Log(logstatus, $"Test ended with status - [ <b>{logstatus}</b> ]");

            _parentTest.Log(Status.Info, "End Test Time");
        }


        public void CreateReport()
        {
            _loggerHelper.LogEnteringTheMethod();

            try
            {
                TimeTestStop = DateTime.Now;
                _report.AddSystemInfo("End Time Project", TimeTestStop.ToString());

                _logger.Trace("Before Flush");
                _report.Flush();
                _logger.Trace("After Flush");
            }
            catch (ArgumentException ex)
            {
                _logger.Error($"ArgumentException during report flush: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception during report flush: {ex.Message}");
            }

            _loggerHelper.LogExitingTheMethod();
        }

        /// <summary>
        /// TO DO  -  Method add styles in ExtentSparkReporter, needed fix
        /// </summary>
        public void ReplaceCssStyleDir()
        {
            _loggerHelper.LogEnteringTheMethod();

            string sourcePath = ".\\extent";
            string destinationPath = "logs\\ReportResults\\extent";

            try
            {
                if (!Directory.Exists(destinationPath))
                {
                    //Directory.Move(sourcePath, destinationPath);
                    CopyDirectory(sourcePath, destinationPath);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception in replace css style: {ex.Message}");
            }
        }

        static void CopyDirectory(string sourceDir, string destinationDir)
        {
            Directory.CreateDirectory(destinationDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (var dir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }

    }
}
