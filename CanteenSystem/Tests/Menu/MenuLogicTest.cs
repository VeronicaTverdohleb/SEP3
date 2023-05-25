using Application.DaoInterfaces;
using Application.Logic;
using Shared.Dtos;
using Shared.Model;

namespace Tests.Menu;

using Moq;

[TestFixture]
public class MenuLogicTest
{
    private Mock<IMenuDao> menuDaoMock;
    private Mock<IItemDao> itemDaoMock;

    private MenuLogic menuLogic;

    [SetUp]
    public void Setup()
    {
        menuDaoMock = new Mock<IMenuDao>();
        itemDaoMock = new Mock<IItemDao>();
        menuLogic = new MenuLogic(menuDaoMock.Object, itemDaoMock.Object);
    }
    
    // Tests for CreateAsync in MenuLogic
    [Test]
    public void CreateAsyncTest_0()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        Shared.Model.Menu menu = new Shared.Model.Menu(date, new List<Shared.Model.Item>());
        MenuBasicDto menuDto = new MenuBasicDto(new List<ItemMenuDto>(), date);

        // Act
        menuDaoMock.Setup(m => m.CreateAsync(menu)).Returns(Task.FromResult(menu));
        
        // Assert
        Assert.DoesNotThrowAsync(() => menuLogic.CreateAsync(menuDto));
    }
    
    [Test]
    public void CreateAsyncTest_M()
    {
        // Arrange
        DateOnly date1 = new DateOnly(2023, 05, 25);
        DateOnly date2 = new DateOnly(2023, 06, 20);
        Shared.Model.Menu menu1 = new Shared.Model.Menu(date1, new List<Shared.Model.Item>());
        Shared.Model.Menu menu2 = new Shared.Model.Menu(date2, new List<Shared.Model.Item>());

        MenuBasicDto menuDto1 = new MenuBasicDto(new List<ItemMenuDto>(), date1);
        MenuBasicDto menuDto2 = new MenuBasicDto(new List<ItemMenuDto>(), date2);

        // Act
        menuDaoMock.Setup(m => m.CreateAsync(menu1));
        menuDaoMock.Setup(m => m.CreateAsync(menu2));
        
        // Assert
        Assert.DoesNotThrowAsync(() => menuLogic.CreateAsync(menuDto1));
        Assert.DoesNotThrowAsync(() => menuLogic.CreateAsync(menuDto2));
    }
    
    [Test]
    public void CreateAsyncTest_E()
    {
        // Arrange
        DateOnly date1 = new DateOnly(2023, 05, 25);
        Shared.Model.Menu menu1 = new Shared.Model.Menu(date1, new List<Shared.Model.Item>());

        MenuBasicDto menuDto1 = new MenuBasicDto(new List<ItemMenuDto>(), date1);

        // Act
        menuDaoMock.Setup(m => m.CreateAsync(menu1)).Returns(Task.FromResult(menu1));
        // The Logic checks if there is a Menu under this date and the method in Dao returns the same Menu object that we are trying to create
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date1)).Returns(Task.FromResult<MenuBasicDto?>(menuDto1));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => menuLogic.CreateAsync(menuDto1));
        Assert.That(e.Message, Is.EqualTo("There is already Menu on this date"));
    }
    
    // Tests for UpdateMenuAsync
    [Test]
    public void UpdateMenuAsync_O()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        List<ItemMenuDto> items = new List<ItemMenuDto>();
        ItemMenuDto itemMenuDto = new ItemMenuDto(1, 1, "Sandwich", "Bread, Salami", "1, 2");
        items.Add(itemMenuDto);
        MenuBasicDto menu = new MenuBasicDto(items, date);
        Shared.Model.Item item = new Shared.Model.Item();
        MenuUpdateDto updateDto = new MenuUpdateDto(date, 1, "remove");

        // Act
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date)).Returns(Task.FromResult<MenuBasicDto?>(menu));
        menuDaoMock.Setup(m => m.UpdateMenuAsync(updateDto));
        itemDaoMock.Setup(i => i.GetByIdAsync(1)).Returns(Task.FromResult<Shared.Model.Item?>(item));
        
        // Assert
        Assert.DoesNotThrowAsync(() => menuLogic.UpdateMenuAsync(updateDto));
    }
    
    [Test]
    public void UpdateMenuAsync_M()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        List<ItemMenuDto> items = new List<ItemMenuDto>();
        ItemMenuDto itemMenuDto = new ItemMenuDto(1, 1, "Sandwich", "Bread, Salami", "1, 2");
        items.Add(itemMenuDto);
        MenuBasicDto menu = new MenuBasicDto(items, date);
        Shared.Model.Item item1 = new Shared.Model.Item();
        Shared.Model.Item item2 = new Shared.Model.Item();
        MenuUpdateDto updateDto1 = new MenuUpdateDto(date, 1, "remove");
        MenuUpdateDto updateDto2 = new MenuUpdateDto(date, 2, "add");
        
        // Act
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date)).Returns(Task.FromResult<MenuBasicDto?>(menu));
        itemDaoMock.Setup(i => i.GetByIdAsync(1)).Returns(Task.FromResult<Shared.Model.Item?>(item1));
        itemDaoMock.Setup(i => i.GetByIdAsync(2)).Returns(Task.FromResult<Shared.Model.Item?>(item2));
        menuDaoMock.Setup(m => m.UpdateMenuAsync(updateDto1));
        menuDaoMock.Setup(m => m.UpdateMenuAsync(updateDto2));
        
        // Assert
        Assert.DoesNotThrowAsync(() => menuLogic.UpdateMenuAsync(updateDto1));
        Assert.DoesNotThrowAsync(() => menuLogic.UpdateMenuAsync(updateDto2));
    }
    
    [Test]
    public void UpdateMenuAsync_E()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        MenuBasicDto menu = new MenuBasicDto(new List<ItemMenuDto>(), date);
        MenuUpdateDto updateDto1 = new MenuUpdateDto(date, 2, "add");
        
        // Act
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date)).Returns(Task.FromResult<MenuBasicDto?>(menu));
        menuDaoMock.Setup(m => m.UpdateMenuAsync(updateDto1)).Returns(Task.FromResult(updateDto1));
        
        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => menuLogic.UpdateMenuAsync(updateDto1));
        Assert.That(e.Message, Is.EqualTo($"You cannot add/remove this Item to the Menu because Item with ID {updateDto1.ItemId} was not found"));
    }
    
    // Tests for GetMenuByDateAsync method in MenuLogic
    [Test]
    public void GetMenuByDateAsyncTest_Z()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        
        // Act 
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => menuLogic.GetMenuByDateAsync(date));
        Assert.That(e.Message, Is.EqualTo("There is no Menu on this date"));
    }

    [Test]
    public void GetMenuByDateAsyncTest_O()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        List<ItemMenuDto> items = new List<ItemMenuDto>();
        ItemMenuDto item = new ItemMenuDto(1, 1, "Sandwich", "Bread, Salami", "1, 2");
        items.Add(item);
        MenuBasicDto menu = new MenuBasicDto(items, date);

        // Act
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date)).Returns(Task.FromResult<MenuBasicDto?>(menu));
        
        // Assert
        Assert.That(menuLogic.GetMenuByDateAsync(date).Result, Is.EqualTo(menu));
    }
    
    [Test]
    public void GetMenuByDateAsyncTest_M()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        List<ItemMenuDto> items = new List<ItemMenuDto>();
        ItemMenuDto item1 = new ItemMenuDto(1, 1, "Sandwich", "Bread, Salami", "1, 2");
        ItemMenuDto item2 = new ItemMenuDto(2, 1, "Broccoli Soup", "Broccoli, Cream", "1");
        ItemMenuDto item3 = new ItemMenuDto(3, 1, "Lasagne", "Pasta, Red Sauce", "4");

        items.Add(item1);
        items.Add(item2);
        items.Add(item3);
        
        MenuBasicDto menu = new MenuBasicDto(items, date);

        // Act
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date)).Returns(Task.FromResult<MenuBasicDto?>(menu));
        
        // Assert
        Assert.That(menuLogic.GetMenuByDateAsync(date).Result.Items, Is.Not.Null);
        Assert.That(menuLogic.GetMenuByDateAsync(date).Result.Items.Count, Is.EqualTo(3));
    }
    
    [Test]
    public void GetMenuByDateAsyncTest_E_NoItems()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        MenuBasicDto menu = new MenuBasicDto(new List<ItemMenuDto>(), date);

        // Act
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date)).Returns(Task.FromResult<MenuBasicDto?>(menu));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => menuLogic.GetMenuByDateAsync(date));
        Assert.That(e.Message, Is.EqualTo("There are no Items on this Menu"));
    }
    
    [Test]
    public void GetMenuByDateAsyncTest_E_NoMenu()
    {
        // Arrange
        DateOnly date = new DateOnly(2023, 05, 25);
        
        // Act 
        menuDaoMock.Setup(m => m.GetMenuByDateAsync(date));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => menuLogic.GetMenuByDateAsync(date));
        Assert.That(e.Message, Is.EqualTo("There is no Menu on this date"));
    }
}