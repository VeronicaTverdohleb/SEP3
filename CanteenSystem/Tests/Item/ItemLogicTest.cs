using System.Transactions;
using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Castle.Components.DictionaryAdapter;
using EfcDataAccess;
using Microsoft.EntityFrameworkCore;
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
    public async Task CreateItem_Z()
    {
        List<int> ingredientsId = new List<int>();
        ingredientsId.Add(0);
        //ingredientsId.Add(2);
        List<Ingredient> ingredients = new List<Ingredient>();
        creationDto = new ItemCreationDto("Cake",22.9,ingredientsId);
        item = new Shared.Model.Item("Chocolate", 12,ingredients);
        
       
        itemDao.Setup(p => p.CreateAsync(creationDto)).Returns(Task.FromResult(item));
        
        item = await  itemDao.Object.CreateAsync(creationDto);
        
        Assert.That(() => itemLogic.CreateAsync(creationDto).Result, 
            Throws.Exception);//.EqualTo(new Exception("This ingredients you try to use, does not exist!")));
       var e = Assert.ThrowsAsync<Exception>(() => itemLogic.CreateAsync(creationDto));
       Assert.That(e.Message,Is.EqualTo("This ingredient you try to use, does not exist!"));
    }
    
    [Test]
    public async Task CreateItem_O()
    {
        List<int> ingredientsId = new List<int>();
        Ingredient? ing = new Ingredient("Tomato",200,0);
        ingredientsId.Add(1);
        creationDto = new ItemCreationDto("Cake",22.9,ingredientsId);
        
        
        itemDao.Setup(p => p.CreateAsync(creationDto)).Returns(Task.FromResult(item));
        foreach (int ingredientId in creationDto.IngredientIds)
        {
            ing = await ingredientDao.Object.GetByIdAsync(ingredientId);
            
        }
        ingredientDao.Setup(i => i.GetByIdAsync(1)).Returns(Task.FromResult(ing)!);


        item = await  itemDao.Object.CreateAsync(creationDto);
        Assert.DoesNotThrowAsync(() => itemDao.Object.CreateAsync(creationDto));

    }
    
    [Test]
    public async Task CreateItem_M()
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        Ingredient? ing = new Ingredient("Tomato",200,0);
        ing = await ingredientDao.Object.GetByIdAsync(1);
        ingredients.Add(ing);
        Ingredient ing2 = new Ingredient("Cheese",200,1);
        ingredients.Add(ing2);
        List<int> ingredientsId = new List<int>();
        ingredientsId.Add(1);
        ingredientsId.Add(2);
       
        
        creationDto = new ItemCreationDto("Cake",22.9,ingredientsId);
        item = new Shared.Model.Item("Sandwich", 12,ingredients);

        itemDao.Setup(p => p.CreateAsync(creationDto)).Returns(Task.FromResult(item)!);
        foreach (int ingredientId in creationDto.IngredientIds)
        {
            ing = await ingredientDao.Object.GetByIdAsync(ingredientId);
            
        }
        ingredientDao.Setup(i => i.GetByIdAsync(1)).Returns(Task.FromResult(ing)!);

        item = await  itemDao.Object.CreateAsync(creationDto);
        
        //Assert.That(() => itemLogic.CreateAsync(creationDto), Is.Not.Empty);//.EqualTo(new Exception("This ingredients you try to use, does not exist!")));
       // var e = Assert.ThrowsAsync<Exception>(() => itemLogic.CreateAsync(creationDto));
       // Assert.That(e.Message,Is.EqualTo(""));
       Assert.DoesNotThrowAsync(() => itemDao.Object.CreateAsync(creationDto));

    }
    [Test]
    public async Task GetItems_Z()
    {
        List<int> ingredientsId = new List<int>();
        ingredientsId.Add(1);
        List<Ingredient> ingredients = new List<Ingredient>();
        Ingredient? ing = new Ingredient("Tomato",200,0);
        ing = await ingredientDao.Object.GetByIdAsync(1);
        ingredients.Add(ing);
        item = new Shared.Model.Item("Sandwich", 12,ingredients);
        IEnumerable<Shared.Model.Item> items = new List<Shared.Model.Item>();
        //items.Append(item);
        


        creationDto = new ItemCreationDto("Cake",22.9,ingredientsId);
        searchdto = new SearchItemSto("Sandwich", null);
        
        
        itemDao.Setup(p => p.GetAsync(searchdto)).Returns(Task.FromResult(items));
        items = await  itemDao.Object.GetAsync(searchdto);
        var e = Assert.ThrowsAsync<Exception>(() => itemDao.Object.GetAsync(searchdto));
        Assert.That(e.Message,Is.EqualTo("This ingredient you try to use, does not exist!"));

        
        Assert.ThrowsAsync<Exception>(() => itemDao.Object.GetAsync(searchdto));

    }
    
    [Test]
    public async Task GetItems_O()
    {
        List<int> ingredientsId = new List<int>();
        ingredientsId.Add(1);
        List<Ingredient> ingredients = new List<Ingredient>();
        Ingredient? ing = new Ingredient("Tomato",200,0);
        ing = await ingredientDao.Object.GetByIdAsync(1);
        ingredients.Add(ing);
        item = new Shared.Model.Item("Sandwich", 12,ingredients);
        IEnumerable<Shared.Model.Item> items = new List<Shared.Model.Item>();
        items.Append(item);
        


        creationDto = new ItemCreationDto("Cake",22.9,ingredientsId);
        searchdto = new SearchItemSto("Sandwich", null);
        
        
        itemDao.Setup(p => p.GetAsync(searchdto)).Returns(Task.FromResult(items));
        

        items = await  itemDao.Object.GetAsync(searchdto);
        var e = Assert.ThrowsAsync<Exception>(() => itemDao.Object.GetAsync(searchdto));
        Assert.That(e.Message,Is.EqualTo(""));

        Assert.DoesNotThrowAsync(() => itemDao.Object.GetAsync(searchdto));

    }

    private void DoSomething(int input)
    {
        if (input == null)
        {
            throw new Exception(nameof(input));
            
        }

        
        
    }
}