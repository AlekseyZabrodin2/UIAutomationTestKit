using System.Runtime.CompilerServices;

namespace UiAutoTests.Core
{
    internal interface ILoggerHelper
    {

        public void LogEnteringTheMethod([CallerMemberName] string methodName = "");

        public void LogExitingTheMethod([CallerMemberName] string methodName = "");

    }
}
