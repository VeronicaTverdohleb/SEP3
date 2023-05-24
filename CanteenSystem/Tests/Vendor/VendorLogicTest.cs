using Application.DaoInterfaces;
using Application.Logic;
using BlazorApp.Services;
using BlazorApp.Services.JavaDataAccess;
using Moq;
using Shared.Model;

namespace Tests.Vendor;

[TestFixture]
public class VendorLogicTest
{
    private Mock<IJavaSocketConnection> javaSocketMock;

    private JavaSocketConnection socketConnection;
    
    [SetUp]
    public void Setup()
    {
        javaSocketMock = new Mock<IJavaSocketConnection>();
        socketConnection = new JavaSocketConnection();
    }

    [Test]
    public void Connect()
    {
        // Arrange, Act & Assert
        Assert.DoesNotThrow(() =>
        {
            socketConnection.Connect();
        });
    }

    [Test]
    public void GetAllVendorsForItem_Z()
    {
        // Arrange
        string name = "";

        // Act
        javaSocketMock.Setup(i => i.Connect());

        // Assert
        var e = Assert.ThrowsAsync<Exception>(async () =>
        {
            socketConnection.Connect();
            await socketConnection.SendMessage(name);
        });
        Assert.That(e.Message, Is.EqualTo("ingredient name cannot be empty!"));
    }
    
    [Test]
    public void GetAllVendorsForItem_O()
    {
        // Arrange
        string name = "Tomato";

        // Act
        javaSocketMock.Setup(i => i.Connect());

        // Assert
        Assert.DoesNotThrowAsync(async () =>
        {
            socketConnection.Connect();
            await socketConnection.SendMessage(name);
        });
    }
}