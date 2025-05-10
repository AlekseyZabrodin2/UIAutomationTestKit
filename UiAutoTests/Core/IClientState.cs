using FlaUI.Core.AutomationElements;

namespace UiAutoTests.Core
{
    public interface IClientState
    {
        string Name { get; }

        Task<IClientState> GoToStateAsync(string stateName, TimeSpan timeout);

        Window GetMainWindow();

        bool IsState(Window window);

    }
}
