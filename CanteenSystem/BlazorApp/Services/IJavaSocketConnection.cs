namespace BlazorApp.Services;

/// <summary>
/// Interface that JavaSocketConnection implements
/// </summary>
public interface IJavaSocketConnection
{
    public void Connect();
    public Task<string> SendMessage(string message);
}