using Application.DaoInterfaces;
using Application.Logic;
using Moq;
using Shared.Dtos;
using Shared.Model;

namespace Tests.Order;

[TestFixture]
public class OrderLogicTest
{
    private Mock<IOrderDao> orderDaoMock;
    private Mock<IItemDao> itemDaoMock;
    private Mock<IUserDao> userDaoMock;

    private OrderLogic orderLogic;
    
    [SetUp]
    public void Setup()
    {
        orderDaoMock = new Mock<IOrderDao>();
        itemDaoMock = new Mock<IItemDao>();
        userDaoMock = new Mock<IUserDao>();
        orderLogic = new OrderLogic(orderDaoMock.Object, itemDaoMock.Object, userDaoMock.Object);
    }

    [Test]
    public void GetOrderByIdTest_Z()
    {
        // Arrange
        int id = 1;
        
        // Act 
        orderDaoMock.Setup(o => o.GetByIdAsync(id));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => orderLogic.GetOrderByIdAsync(id));
        Assert.That(e.Message, Is.EqualTo("Order with id 1 not found"));
    }
    
    [Test]
    public void GetOrderByIdTest_O()
    {
        // Arrange
        int id = 1;
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        Shared.Model.Item item = new Shared.Model.Item("test", 25, new List<Ingredient>());
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Add(item);
        Shared.Model.Order order = new Shared.Model.Order(user,date,"ordered",items);
        
        // Act
        orderDaoMock.Setup(o => o.GetByIdAsync(id)).Returns(Task.FromResult<Shared.Model.Order?>(order));
        
        // Assert
        Assert.That(orderLogic.GetOrderByIdAsync(id).Result, Is.EqualTo(order));
    }

    [Test]
    public void getAllOrders_E()
    {
        //Arrange
        SearchOrderParametersDto searchDto = new SearchOrderParametersDto(null, null, null, null);
        
        //Act
        orderDaoMock.Setup(o => o.GetAllOrdersAsync(searchDto)).Returns(Task.FromResult<IEnumerable<Shared.Model.Order>>(null!));
        
        //Assert
        Assert.DoesNotThrowAsync(()=>orderLogic.GetAllOrdersAsync(searchDto));
    }
    
    [Test]
    public void getAllOrders_Z()
    {
        //Arrange
        SearchOrderParametersDto searchDto = new SearchOrderParametersDto(null, null, null, null);
        
        //Act
        orderDaoMock.Setup(o => o.GetAllOrdersAsync(searchDto));
        
        //Assert
        Assert.DoesNotThrowAsync(()=>orderLogic.GetAllOrdersAsync(searchDto));
    }
    
    [Test]
    public void getAllOrders_O()
    {
        //Arrange
        SearchOrderParametersDto searchDto = new SearchOrderParametersDto(null, null, null, null);
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        Shared.Model.Item item = new Shared.Model.Item("test", 25, new List<Ingredient>());
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Add(item);
        Shared.Model.Order order = new Shared.Model.Order(user,date,"ordered",items);
        List<Shared.Model.Order> orders = new List<Shared.Model.Order>();
        orders.Add(order);
        
        //Act
        orderDaoMock.Setup(o => o.GetAllOrdersAsync(searchDto)).Returns(Task.FromResult<IEnumerable<Shared.Model.Order>>(orders));
        
        //Assert
        Assert.That(orderLogic.GetAllOrdersAsync(searchDto).Result, Is.EqualTo(orders));
    }

    [Test]
    public void getAllOrders_M()
    {
        //Arrange
        SearchOrderParametersDto searchDto = new SearchOrderParametersDto(null, null, null, null);
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        Shared.Model.Item item = new Shared.Model.Item("test", 25, new List<Ingredient>());
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Add(item);
        Shared.Model.Order order = new Shared.Model.Order(user, date, "ordered", items);
        List<Shared.Model.Order> orders = new List<Shared.Model.Order>();
        orders.Add(order);
        orders.Add(order);

        //Act
        orderDaoMock.Setup(o => o.GetAllOrdersAsync(searchDto))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Order>>(orders));

        //Assert
        Assert.That(orderLogic.GetAllOrdersAsync(searchDto).Result, Is.EqualTo(orders));
    }

    [Test]
    public void GetAllOrdersWithAllSearchParameters()
    {
        //Arrange
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        Shared.Model.Item item = new Shared.Model.Item("test", 25, new List<Ingredient>());
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Add(item);
        Shared.Model.Order order = new Shared.Model.Order(user, date, "ordered", items);
        List<Shared.Model.Order> orders = new List<Shared.Model.Order>();
        orders.Add(order);
        orders.Add(order);
        SearchOrderParametersDto searchDto = new SearchOrderParametersDto(order.Id, date, user.UserName, order.Status);

        //Act
        orderDaoMock.Setup(o => o.GetAllOrdersAsync(searchDto))
            .Returns(Task.FromResult<IEnumerable<Shared.Model.Order>>(orders));

        //Assert
        Assert.That(orderLogic.GetAllOrdersAsync(searchDto).Result, Is.EqualTo(orders));
    }

    [Test]
    public void DeleteOrder_Z()
    {
        //Arrange
        int id = 1;
        
        //Act
        orderDaoMock.Setup(o => o.DeleteOrderAsync(id));
        
        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => orderLogic.DeleteOrderAsync(id));
        Assert.That(e.Message, Is.EqualTo("Order with ID 1 was not found!"));
    }
    
    [Test]
    public void DeleteOrder_O()
    {
        //Arrange
        int id = 1;
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        Shared.Model.Item item = new Shared.Model.Item("test", 25, new List<Ingredient>());
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Add(item);
        Shared.Model.Order order = new Shared.Model.Order(user, date, "ordered", items);
        
        //Act
        orderDaoMock.Setup(o => o.GetByIdAsync(id)).Returns(Task.FromResult<Shared.Model.Order?>(order));
        
        //Assert
        Assert.DoesNotThrowAsync(()=>orderLogic.DeleteOrderAsync(id));
    }
    
    [Test]
    public void UpdateOrder_Z()
    {
        // Arrange
        Shared.Model.Item item = new Shared.Model.Item("test", 25, new List<Ingredient>());
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Add(item);
        OrderUpdateDto updateDto = new OrderUpdateDto(1, items, "in progress");

        // Act
        orderDaoMock.Setup(o => o.GetByIdAsync(1)).Returns(Task.FromResult<Shared.Model.Order?>(null));

        // Assert
        var e = Assert.ThrowsAsync<Exception>(() => orderLogic.UpdateOrderAsync(updateDto));
        Assert.That(e.Message, Is.EqualTo("Order with ID 1 not found!"));
    }
    
    [Test]
    public void UpdateOrder_O()
    {
        //Arrange
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        Shared.Model.Item item = new Shared.Model.Item("test", 25, new List<Ingredient>());
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Add(item);
        Shared.Model.Order order = new Shared.Model.Order(user, date, "ordered", items);
        OrderUpdateDto updateDto = new OrderUpdateDto(1, items, "in progress");
        
        //Act
        orderDaoMock.Setup(o => o.GetByIdAsync(1)).Returns(Task.FromResult<Shared.Model.Order?>(order));

        //Assert
        Assert.DoesNotThrowAsync(()=>orderLogic.UpdateOrderAsync(updateDto));
    }
    
    [Test]
    public void UpdateOrderWithMoreThanOneItem()
    {
        //Arrange
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        Shared.Model.Item item = new Shared.Model.Item("test", 25, new List<Ingredient>());
        Shared.Model.Item item2 = new Shared.Model.Item("test", 25, new List<Ingredient>());
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Add(item);
        items.Add(item2);
        Shared.Model.Order order = new Shared.Model.Order(user, date, "ordered", items);
        OrderUpdateDto updateDto = new OrderUpdateDto(1, items, "in progress");
        
        //Act
        orderDaoMock.Setup(o => o.GetByIdAsync(1)).Returns(Task.FromResult<Shared.Model.Order?>(order));

        //Assert
        Assert.DoesNotThrowAsync(()=>orderLogic.UpdateOrderAsync(updateDto));
    }
    
    [Test]
    public void UpdateOrderWithNoItems()
    {
        //Arrange
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        Shared.Model.Order order = new Shared.Model.Order(user, date, "ordered", items);
        OrderUpdateDto updateDto = new OrderUpdateDto(1, items, "in progress");
        
        //Act
        orderDaoMock.Setup(o => o.GetByIdAsync(1)).Returns(Task.FromResult<Shared.Model.Order?>(order));

        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => orderLogic.UpdateOrderAsync(updateDto));
        Assert.That(e.Message, Is.EqualTo("Order will be empty! delete instead!"));
    }

    [Test]
    public void UpdateCompletedOrder()
    {
        //Arrange
        DateOnly date = new DateOnly(2063, 05, 25);
        User user = new User("Steve", "Stevenson", "Steve", "hello", "Steve@hotmail.com", "CanteenEmployee");
        List<Shared.Model.Item> items = new List<Shared.Model.Item>();
        Shared.Model.Order order = new Shared.Model.Order(user, date, "ready for pickup", items);
        OrderUpdateDto updateDto = new OrderUpdateDto(1, items, "in progress");
        
        //Act
        orderDaoMock.Setup(o => o.GetByIdAsync(1)).Returns(Task.FromResult<Shared.Model.Order?>(order));

        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => orderLogic.UpdateOrderAsync(updateDto));
        Assert.That(e.Message, Is.EqualTo("Cannot change status of an order that is ready for pickup!"));
    }
}