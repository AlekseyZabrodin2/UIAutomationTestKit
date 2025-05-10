namespace UiAutoTests.Core
{
    public interface ITestClient
    {
        Task<IClientState> StartAsync(TimeSpan timeout);

        void Kill();
    }
}
