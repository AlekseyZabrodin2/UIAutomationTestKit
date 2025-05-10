namespace UiAutoTests.Core
{
    internal interface IReporter
    {
        public void ReportLogger(string testCaseName);

        public void InitializeTests(string testContext, string tag);

        public void CreateTest(string testContext);

        public void CreateNode(string node);

        public void CreateTags(string tags);

        public void LogStatusPass(string status);

        public void LogStatusFail(Exception exception, string status);

        public void GetTestsStatus();

        public void CreateReport();
    }
}
