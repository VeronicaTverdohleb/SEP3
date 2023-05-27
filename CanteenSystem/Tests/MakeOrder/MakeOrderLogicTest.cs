using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Moq;
using Shared.Dtos;
using Shared.Model;

namespace Tests.MakeOrder;
[TestFixture]

public class MakeOrderLogicTest
{
    private Mock<IOrderDao>  orderDao;
    private Mock<IItemDao>  itemDao;
    private Mock<IUserDao>  userDao;
    private IOrderLogic orderLogic;
    
    [SetUp]
    public void Setup()
    {
        orderDao = new Mock<IOrderDao>();
        itemDao = new Mock<IItemDao>();
        userDao = new Mock<IUserDao>();
        orderLogic = new OrderLogic(orderDao.Object, itemDao.Object, userDao.Object);
        
    }

    [Test]
    public void CreateOrder_Z()
    {
        User user = new User("Steve", "Steve", "Steve", "hello", "steve@gmail.com", "VIAStudent")
        {
            Id = 1
        };
        DateOnly dateOnly = new DateOnly(2023, 07, 25);
        Shared.Model.Order order = new Shared.Model.Order(user,dateOnly,"ordered",new List<Shared.Model.Item>());
        MakeOrderDto makeOrderDto = new MakeOrderDto(1,dateOnly,"ordered", new List<int>());
        orderDao.Setup(o => o.CreateOrderAsync(makeOrderDto)).Returns(Task.FromResult(order));
        Assert.That(() => orderLogic.CreateOrderAsync(makeOrderDto).Result, Throws.Exception);//.EqualTo(new Exception("This ingredients you try to use, does not exist!")));
        var e = Assert.ThrowsAsync<Exception>(() => orderLogic.CreateOrderAsync(makeOrderDto));
        Assert.That(e.Message,Is.EqualTo("An order needs to have items"));

    }
    
    [Test]
    public void CreateOrder_OneItem()
    {
        User user = new User("Steve", "Steve", "Steve", "hello", "steve@gmail.com", "VIAStudent")
        {
            Id = 1
        };
        DateOnly dateOnly = new DateOnly(2023, 07, 25);
        Ingredient n = new Ingredient("Cucumber", 200, 0)
        {
            Id = 1
        };
        List<Ingredient> ingredients = new List<Ingredient> { n };

        Shared.Model.Item item1 = new Shared.Model.Item("Something", 22.9,ingredients)
            {Id = 1};
        List<int> imteIds = new List<int>() { item1.Id };
        List<Shared.Model.Item> items = new List<Shared.Model.Item> { item1 };

        Shared.Model.Order order = new Shared.Model.Order(user,dateOnly,"ordered",items);
        MakeOrderDto makeOrderDto = new MakeOrderDto(1,dateOnly,"ordered", imteIds);
        orderDao.Setup(o => o.CreateOrderAsync(makeOrderDto)).Returns(Task.FromResult(order));
        Assert.DoesNotThrowAsync(()=>orderLogic.CreateOrderAsync(makeOrderDto));

    }
    
    [Test]
    public void CreateOrder_MultipleItems()
    {
        User user = new User("Steve", "Steve", "Steve", "hello", "steve@gmail.com", "VIAStudent")
        {
            Id = 1
        };
        DateOnly dateOnly = new DateOnly(2023, 07, 25);
        Ingredient n = new Ingredient("Cucumber", 200, 0)
        {
            Id = 1
        };
        Ingredient n1 = new Ingredient("Tomato", 200, 0)
        {
            Id = 2
        };
        List<Ingredient> ingredients = new List<Ingredient> { n, n1};

        Shared.Model.Item item1 = new Shared.Model.Item("Something", 22.9,ingredients)
            {Id = 1};
        Shared.Model.Item item2 = new Shared.Model.Item("Something Else", 22.9,ingredients)
            {Id = 2};
        List<int> imteIds = new List<int>() { item1.Id, item2.Id };
        List<Shared.Model.Item> items = new List<Shared.Model.Item> { item1 };

        Shared.Model.Order order = new Shared.Model.Order(user,dateOnly,"ordered",items);
        MakeOrderDto makeOrderDto = new MakeOrderDto(1,dateOnly,"ordered", imteIds);
        orderDao.Setup(o => o.CreateOrderAsync(makeOrderDto)).Returns(Task.FromResult(order));
        Assert.DoesNotThrowAsync(()=>orderLogic.CreateOrderAsync(makeOrderDto));

    }
    
    [Test]
    public void CreateOrder_MultipleOrders()
    {
        User user = new User("Steve", "Steve", "Steve", "hello", "steve@gmail.com", "VIAStudent")
        {
            Id = 1
        };
        DateOnly dateOnly = new DateOnly(2023, 07, 25);
        Ingredient n = new Ingredient("Cucumber", 200, 0)
        {
            Id = 1
        };
        List<Ingredient> ingredients = new List<Ingredient> { n };

        Shared.Model.Item item1 = new Shared.Model.Item("Something", 22.9,ingredients)
            {Id = 1};
        List<int> imteIds = new List<int>() { item1.Id };
        List<Shared.Model.Item> items = new List<Shared.Model.Item> { item1 };

        Shared.Model.Order order = new Shared.Model.Order(user,dateOnly,"ordered",items);
        MakeOrderDto makeOrderDto = new MakeOrderDto(1,dateOnly,"ordered", imteIds);
        Shared.Model.Order order2 = new Shared.Model.Order(user,dateOnly,"ordered",items);
        MakeOrderDto makeOrderDto2 = new MakeOrderDto(1,dateOnly,"ordered", imteIds);

        orderDao.Setup(o => o.CreateOrderAsync(makeOrderDto)).Returns(Task.FromResult(order));
        orderDao.Setup(o => o.CreateOrderAsync(makeOrderDto2)).Returns(Task.FromResult(order2));

        Assert.DoesNotThrowAsync(()=>orderLogic.CreateOrderAsync(makeOrderDto));
        Assert.DoesNotThrowAsync(()=>orderLogic.CreateOrderAsync(makeOrderDto2));

    }
    
    [Test]
    public void CreateOrder_OneItemPastDate()
    {
        User user = new User("Steve", "Steve", "Steve", "hello", "steve@gmail.com", "VIAStudent")
        {
            Id = 1
        };
        DateOnly dateOnly = new DateOnly(2023, 05, 25);
        Ingredient n = new Ingredient("Cucumber", 200, 0)
        {
            Id = 1
        };
        List<Ingredient> ingredients = new List<Ingredient> { n };

        Shared.Model.Item item1 = new Shared.Model.Item("Something", 22.9,ingredients)
            {Id = 1};
        List<int> imteIds = new List<int>() { item1.Id };
        List<Shared.Model.Item> items = new List<Shared.Model.Item> { item1 };

        Shared.Model.Order order = new Shared.Model.Order(user,dateOnly,"ordered",items);
        MakeOrderDto makeOrderDto = new MakeOrderDto(1,dateOnly,"ordered", imteIds);
        orderDao.Setup(o => o.CreateOrderAsync(makeOrderDto)).Returns(Task.FromResult(order));
        var e = Assert.ThrowsAsync<Exception>(() => orderLogic.CreateOrderAsync(makeOrderDto));
        Assert.That(e.Message,Is.EqualTo("You cannot create an order on this date"));
       

    }
    
    [Test]
    public void CreateOrder_OneItemCurrentDateAfter12()
    {
        User user = new User("Steve", "Steve", "Steve", "hello", "steve@gmail.com", "VIAStudent")
        {
            Id = 1
        };
        TimeOnly time = new TimeOnly(12,00,00);
        DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now.ToLocalTime());
        Ingredient n = new Ingredient("Cucumber", 200, 0)
        {
            Id = 1
        };
        List<Ingredient> ingredients = new List<Ingredient> { n };

        Shared.Model.Item item1 = new Shared.Model.Item("Something", 22.9,ingredients)
            {Id = 1};
        List<int> imteIds = new List<int>() { item1.Id };
        List<Shared.Model.Item> items = new List<Shared.Model.Item> { item1 };

        Shared.Model.Order order = new Shared.Model.Order(user,dateOnly,"ordered",items);
        MakeOrderDto makeOrderDto = new MakeOrderDto(1,dateOnly,"ordered", imteIds);
        orderDao.Setup(o => o.CreateOrderAsync(makeOrderDto)).Returns(Task.FromResult(order));
        if (DateTime.Now.ToLocalTime().Hour >= 12.00)
        {
            var e = Assert.ThrowsAsync<Exception>(() => orderLogic.CreateOrderAsync(makeOrderDto));
            Assert.That(e.Message,Is.EqualTo("You cannot create an order today because it is past 12PM"));
            Console.WriteLine("here2");
        }

        Assert.DoesNotThrowAsync(()=>orderLogic.CreateOrderAsync(makeOrderDto));
        Console.WriteLine("here");
       

    }
    
    
  
    
}