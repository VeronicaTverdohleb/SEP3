namespace BlazorApp.Services;

public interface IJavaSocketConnection
{
    public void Connect();
    public Task<string> SendMessage(string message);
}