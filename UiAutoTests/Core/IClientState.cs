using FlaUI.Core.AutomationElements;

namespace UiAutoTests.Core
{
    public interface IClientState
    {
        string Name { get; }

        Task<IClientState> GoToStateAsync(string stateName, TimeSpan timeout);

        Window GetMainWindow();

        IClientState ToNextState();

        bool IsState(Window window);

    }
}
