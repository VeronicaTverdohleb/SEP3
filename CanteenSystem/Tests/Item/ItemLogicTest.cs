using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Moq;
using Shared.Dtos;
using Shared.Model;


namespace Tests.Item;

[TestFixture]
public class ItemLogicTest
{
    
    private Mock<IIngredientDao> ingredientDao;
    private Mock<IItemDao>  itemDao;
    private SearchItemSto searchdto;
    private Shared.Model.Item item;
    private ItemCreationDto creationDto;
    private IItemLogic itemLogic;
    
    
    [SetUp]
    public void Setup()
    {
       
        item = new Shared.Model.Item();
        itemDao = new Mock<IItemDao>();
        ingredientDao = new Mock<IIngredientDao>();
        itemLogic = new ItemLogic(itemDao.Object,ingredientDao.Object);
       

    }

   
    
    [Test]
    public void CreateItem_Z()
    {
        // Arrange
        creationDto = new ItemCreationDto("Cake",22.9,new List<int>());
        item = new Shared.Model.Item("Cake", 22.9,new List<Ingredient>());
        
        // Act
        itemDao.Setup(p => p.CreateAsync(creationDto)).Returns(Task.FromResult(item));
        
        // Assert
       Assert.That(() => itemLogic.CreateAsync(creationDto).Result, Throws.Exception);
       var e = Assert.ThrowsAsync<Exception>(() => itemLogic.CreateAsync(creationDto));
       Assert.That(e.Message,Is.EqualTo("An item needs to have ingredients"));
    }
    
    [Test]
    public void CreateItem_O()
    {
        Ingredient n = new Ingredient("Cucumber", 200, 0)
        {
            Id = 1
        };
        List<int> ingredientsId = new List<int> { n.Id };
        List<Ingredient> ingredients = new List<Ingredient> { n };
        Shared.Model.Item item1 = new Shared.Model.Item("Something", 22.9,ingredients);
        creationDto = new ItemCreationDto("Something",22.9,ingredientsId);


        itemDao.Setup(p => p.CreateAsync(creationDto)).Returns(Task.FromResult(item1));

        Assert.DoesNotThrowAsync(()=>itemLogic.CreateAsync(creationDto));
        

       
    }
    
    [Test]
    public async Task CreateItem_ManyItems()
    {
        List<int> ingredientsId = new List<int>();
      
        ingredientsId.Add(1);
        Ingredient n = new Ingredient("Cherry", 200, 0);
        List<Ingredient> ingredients = new List<Ingredient>();

        ingredients.Add(n);
        
        Shared.Model.Item item1 = new Shared.Model.Item("Sandwich", 32.02, ingredients);
        Shared.Model.Item item2 = new Shared.Model.Item("Pasta", 28.02, ingredients);
        creationDto = new ItemCreationDto(item1.Name,item1.Price,ingredientsId);
        ItemCreationDto creationDto1 = new ItemCreationDto(item2.Name,item2.Price,ingredientsId);
        creationDto.IngredientIds.Add(1);
        creationDto1.IngredientIds.Add(1);


        itemDao.Setup(i => i.CreateAsync(creationDto)).Returns(Task.FromResult(item1));
        itemDao.Setup(i => i.CreateAsync(creationDto1)).Returns(Task.FromResult(item2));

       
        //Assert.That(() => itemLogic.CreateAsync(creationDto), Is.Not.Empty);//.EqualTo(new Exception("This ingredients you try to use, does not exist!")));
       // var e = Assert.ThrowsAsync<Exception>(() => itemLogic.CreateAsync(creationDto));
       // Assert.That(e.Message,Is.EqualTo(""));
       Assert.DoesNotThrowAsync(() => itemLogic.CreateAsync(creationDto));
       Assert.DoesNotThrowAsync(() => itemLogic.CreateAsync(creationDto1));

    }
    [Test]
    public async Task CreateItem_ItemWithManyIngredients()
    {
        List<int> ingredientsId = new List<int>();
        creationDto = new ItemCreationDto("Cake",22.9,ingredientsId);
        creationDto.IngredientIds.Add(1);
        creationDto.IngredientIds.Add(2);
        foreach (var i in ingredientsId)
        {
            Console.WriteLine(i);  
        }
        Ingredient n = new Ingredient("Tomato", 200, 0);
        Ingredient n1 = new Ingredient("Cherry", 200, 0);
        List<Ingredient> ingredients = new List<Ingredient>();
        ingredients.Add(n);
        ingredients.Add(n1);
        Shared.Model.Item item1 = new Shared.Model.Item("Cake", 22.9,ingredients);
        foreach (var i in ingredients)
        {
            Console.WriteLine(i.Name);  
        }


        itemDao.Setup(p => p.CreateAsync(creationDto)).Returns(Task.FromResult(item1));
        foreach (var i in creationDto.IngredientIds)
        {
            Console.WriteLine(i); 
        }
        Console.WriteLine(creationDto.Name, creationDto.Price.ToString());
        foreach (var i in item1.Ingredients)
        {
            Console.WriteLine(i.Name); 
        }
        Console.WriteLine(item1.Name, item1.Price.ToString());
        //item = await  itemDao.Object.CreateAsync(creationDto);
        Assert.DoesNotThrowAsync(()=>itemLogic.CreateAsync(creationDto));
        

       
    }



    [Test]
    public async Task GetItemsById_Z()
    {
        int id = 15;
        itemDao.Setup(i => i.GetByIdAsync(id));
        var e = Assert.ThrowsAsync<Exception>(() => itemLogic.GetByIdAsync(id));
        Assert.That(e.Message,Is.EqualTo($"Item with ID {id} was not found!"));
    }
    
    [Test]
    public async Task GetItemsById_O()
    {
        Ingredient n = new Ingredient("Tomato", 200, 0);
        n.Id = 1;
        int id = 1;
        ICollection<Ingredient> ingredients = new List<Ingredient>();
        ingredients.Add(n);
        Shared.Model.Item? item1 = new Shared.Model.Item("Sandwich",32.02,ingredients);
        ItemBasicDto? dto = new ItemBasicDto("Sandwich", 32.02, ingredients);
        itemDao.Setup(i => i.GetByIdAsync(id)).Returns(Task.FromResult(item1)!);
        Assert.That(itemLogic.GetByIdAsync(id).Result.Name,Is.EqualTo(dto.Name));
    }
    
    [Test]
    public async Task GetItemsByName_Z()
    {
        string name = "Pasta";
        itemDao.Setup(i => i.GetByNameAsync(name));
        var e = Assert.ThrowsAsync<Exception>(() => itemLogic.GetByNameAsync(name));
        Assert.That(e.Message,Is.EqualTo($"Item with Name {name} was not found!"));
    }
    [Test]
    public async Task GetItemsByName_O()
    {
        string name = "Pasta";
        ICollection<Ingredient> ingredients = new List<Ingredient>();
        Ingredient n = new Ingredient("Tomato", 200, 0);
        ingredients.Add(n);
        Shared.Model.Item item1 = new Shared.Model.Item("Sandwich",32.02,ingredients);
        ItemBasicDto dto = new ItemBasicDto("Sandwich", 32.02, ingredients);

        itemDao.Setup(i => i.GetByNameAsync(name)).Returns(Task.FromResult(item1));
        var getSmth = itemLogic.GetByNameAsync(name).Result;
        foreach (var i in dto.Ingredients)
        {
            Console.WriteLine(i.Name);
        }
        Console.WriteLine(dto.Name+dto.Price+dto.Ingredients);
        foreach (var i in getSmth.Ingredients)
        {
            Console.WriteLine(i.Name);
        }
        Console.WriteLine(getSmth.Name+getSmth.Price+getSmth.Ingredients);
        Assert.That(getSmth.Name,Is.EqualTo(dto.Name));
       
        
    }
  
    
    [Test]
    public void GetItems_Z()
    {
        IEnumerable<Shared.Model.Item> items = new List<Shared.Model.Item>();

        
        searchdto = new SearchItemSto(null, null);
        
        
        itemDao.Setup(p => p.GetAllItemsAsync(searchdto)).Returns(Task.FromResult(items)!);
       
        Assert.DoesNotThrowAsync(() => itemLogic.GetAllItemsAsync(searchdto));

    }
    
    [Test]
    public void GetItems_O()
    {
        List<int> ingredientsId = new List<int> { 1 };
        List<Ingredient> ingredients = new List<Ingredient>();
        Ingredient? ing = new Ingredient("Tomato",200,0);
        ingredients.Add(ing);
        item = new Shared.Model.Item("Sandwich", 12,ingredients);
        IEnumerable<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Append(item);

        creationDto = new ItemCreationDto("Cake",22.9,ingredientsId);
        searchdto = new SearchItemSto("Sandwich", null);
        
        
        itemDao.Setup(p => p.GetAllItemsAsync(searchdto)).Returns(Task.FromResult(items));
        
        Assert.DoesNotThrowAsync(() => itemLogic.GetAllItemsAsync(searchdto));

    }
    
    [Test]
    public void DeleteItems_Z()
    {
        int id = 3;
        
       IEnumerable<Shared.Model.Item> items = new List<Shared.Model.Item>();
       
        itemDao.Setup(p => p.DeleteAsync(id)).Returns(Task.FromResult(items));
        
        var e = Assert.ThrowsAsync<Exception>(() => itemLogic.DeleteAsync(id));
        Assert.That(e.Message,Is.EqualTo($"Item with ID {id} was not found!"));


    }
    
    [Test]
    public void DeleteItems_ItemWithOneIngredient()
    {
        List<int> ingredientsId = new List<int> { 1 };
        List<Ingredient> ingredients = new List<Ingredient>();
        item = new Shared.Model.Item("Sandwich", 12,ingredients)
        {
            Id = 1
        };
        List<Shared.Model.Item> items = new List<Shared.Model.Item> { item };


        itemDao.Setup(p => p.GetByIdAsync(1)).Returns(Task.FromResult(item)!);

        
        Assert.DoesNotThrowAsync(() => itemLogic.DeleteAsync(1));

    }
    
    [Test]
    public void DeleteItems_ItemWithManyIngredient()
    {
        List<int> ingredientsId = new List<int> { 0,1,2 };
        Ingredient n = new Ingredient("Tomato", 200, 0);
        Ingredient n1 = new Ingredient("Cucumber", 200, 0);
        List<Ingredient> ingredients = new List<Ingredient>{n,n1};
        

        item = new Shared.Model.Item("Sandwich", 12,ingredients)
        {
            Id = 1
        };
        List<Shared.Model.Item> items = new List<Shared.Model.Item> { item };


        itemDao.Setup(p => p.GetByIdAsync(1)).Returns(Task.FromResult(item)!);

        
        Assert.DoesNotThrowAsync(() => itemLogic.DeleteAsync(item.Id));

    }

    
}