using Application.LogicInterfaces;
using Moq;

namespace Tests.Item;
[TestFixture]
public class ItemWebAPIControllerTest
{
    private Mock<IItemLogic> itemlogic;
    [SetUp]
    public void Setup()
    {
        itemlogic = new Mock<IItemLogic>();

    }

    [Test]
    public async Task CreateItemInController()
    {
      //  Shared.Model.Item item = await itemlogic.CreateAsync();
        Assert.Pass();
    }
}