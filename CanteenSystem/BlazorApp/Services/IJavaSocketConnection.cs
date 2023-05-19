namespace BlazorApp.Services;

public interface IJavaSocketConnection
{
    public void Connect();
    public Task SendMessage(string message);
}