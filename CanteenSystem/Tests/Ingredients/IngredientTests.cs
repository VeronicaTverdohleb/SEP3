using Application.DaoInterfaces;
using Application.Logic;
using Moq;
using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace Tests.Ingredients;

public class IngredientTests
{
    private Mock<IIngredientDao> mockIngredientSet;
    
    private IngredientLogic ingredientLogic;
    
    [SetUp]
    public void Setup()
    {
        mockIngredientSet = new Mock<IIngredientDao>();
        ingredientLogic = new IngredientLogic(mockIngredientSet.Object);
    }

    // Tests for AddIngredient Method
    [Test]
    public void TestAddIngredient_Z()
    {
        //Arrange
        Ingredient creation = new Ingredient("", 55, 1);
        IngredientCreationDto creationDto = new IngredientCreationDto("", 55, 1);
        //Act
        mockIngredientSet.Setup(I => I.CreateAsync(creation)).Returns(Task.FromResult(creation));
        //Assert
        var e = Assert.ThrowsAsync<Exception>(()=> ingredientLogic.CreateAsync(creationDto));
        Assert.That(e.Message, Is.EqualTo("Name Field Is Required"));
    }
    
    
    [Test]
    public void TestAddIngredient_O()
    {
        //Arrange
        Ingredient creation = new Ingredient("Bread", 55, 1);
        IngredientCreationDto creationDto = new IngredientCreationDto("Bread", 55, 1);
        //Act
        mockIngredientSet.Setup(I => I.CreateAsync(creation)).Returns(Task.FromResult(creation));
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.CreateAsync(creationDto));
    }
    
    [Test]
    public void TestAddIngredient_B()
    {
        //Arrange
        Ingredient creation = new Ingredient("B", 100, 6);
        IngredientCreationDto creationDto = new IngredientCreationDto("B", 100, 6);
        Ingredient creation1 = new Ingredient("Bread Is So Amazing I Cannot Believe It Tastes this", 100, 6);
        IngredientCreationDto creationDto1 = new IngredientCreationDto("Bread Is So Amazing I Cannot Believe It Tastes this", 100, 6);
        
        //Act
        mockIngredientSet.Setup(I => I.CreateAsync(creation)).Returns(Task.FromResult(creation));
        mockIngredientSet.Setup(I => I.CreateAsync(creation1)).Returns(Task.FromResult(creation1));

        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.CreateAsync(creationDto));
        var e = Assert.ThrowsAsync<Exception>(() => ingredientLogic.CreateAsync(creationDto1));
        Assert.That(e.Message, Is.EqualTo("Max Name Length Is 50 Characters"));
    }
    
    //Tests for RemoveIngredient Method
    [Test]
    public void TestRemoveIngredient_Z()
    {
        //Arrange

        //Act
        mockIngredientSet.Setup(I => I.DeleteAsync(1));
        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => ingredientLogic.DeleteIngredient(1));
        Assert.That(e.Message, Is.EqualTo("Ingredient with ID 1 was not found!"));

    }
    
    [Test]
    public void TestRemoveIngredient_0()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Potato", 100, 0);
        //Act
        mockIngredientSet.Setup(I => I.GetByIdAsync(0)).Returns(Task.FromResult(ingredient));
        mockIngredientSet.Setup(I => I.DeleteAsync(0));
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.DeleteIngredient(0));
    }
    
    // Tests for UpdateIngredientAmount Method
    [Test]
    public void TestUpdateIngredient_Z()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("", 0, 0);
        IngredientUpdateDto updateDto = new IngredientUpdateDto(1, 100);
        //Act
        mockIngredientSet.Setup(I => I.UpdateAsync(ingredient)).Returns(Task.FromResult(ingredient));
        //Assert
        var e =Assert.ThrowsAsync<Exception>(() => ingredientLogic.UpdateIngredientAmount(updateDto));
        Assert.That(e.Message, Is.EqualTo("Ingredient with the id 1 was not found!"));
    }
    
    [Test]
    public void TestUpdateIngredient_O()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Cheese", 55, 1);
        IngredientUpdateDto updateDto = new IngredientUpdateDto(1, 45);
        //Act
        mockIngredientSet.Setup(I => I.GetByIdAsync(1)).Returns(Task.FromResult(ingredient)!);
        mockIngredientSet.Setup(I => I.UpdateAsync(ingredient)).Returns(Task.FromResult(ingredient));
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.UpdateIngredientAmount(updateDto));
    }

    [Test]
    public void TestUpdateIngredient_M()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Cheese", 55, 1);
        Ingredient ingredient1 = new Ingredient("Potato", 100, 0);
        IngredientUpdateDto updateDto = new IngredientUpdateDto(1, 45);
        IngredientUpdateDto updateDto1 = new IngredientUpdateDto(2, 65);
        //Act
        mockIngredientSet.Setup(I => I.GetByIdAsync(1)).Returns(Task.FromResult(ingredient)!);
        mockIngredientSet.Setup(I => I.GetByIdAsync(2)).Returns(Task.FromResult(ingredient1)!);
        mockIngredientSet.Setup(I => I.UpdateAsync(ingredient)).Returns(Task.FromResult(ingredient));
        mockIngredientSet.Setup(I => I.UpdateAsync(ingredient1)).Returns(Task.FromResult(ingredient1));
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.UpdateIngredientAmount(updateDto)); 
        Assert.DoesNotThrowAsync(() => ingredientLogic.UpdateIngredientAmount(updateDto1));
    }

    //Tests for GetAsync Method
    [Test]
    public void TestGetAsync_O()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Cheese", 55, 1);
        List<Ingredient> list = new List<Ingredient>();
        list.Add(ingredient);
        //Act
        mockIngredientSet.Setup(I => I.GetAsync()).Returns(Task.FromResult<IEnumerable<Ingredient>>(list));
        //Assert
        Assert.DoesNotThrowAsync(ingredientLogic.GetAsync);
    }

    [Test] 
    public void TestGetAsync_M()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Cheese", 55, 1);
        Ingredient ingredient1 = new Ingredient("Potato", 100, 0);
        Ingredient ingredient2 = new Ingredient("Lettuce", 150, 0);
        List<Ingredient> list = new List<Ingredient>();
        list.Add(ingredient1);
        list.Add(ingredient);
        list.Add(ingredient2);
        //Act
        mockIngredientSet.Setup(I => I.GetAsync()).Returns(Task.FromResult<IEnumerable<Ingredient>>(list));
        //Assert
        Assert.DoesNotThrowAsync(ingredientLogic.GetAsync);
    }

    //Tests for GetByIdAsync Method
    [Test]
    public void TestGetByIdAsync_O()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Cheese", 44, 1);
        
        //Act
        mockIngredientSet.Setup(I => I.GetByIdAsync(ingredient.Id)).Returns(Task.FromResult(ingredient));
        
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.GetByIdAsync(0));

    }

    //Tests for GetByNameAsync Method
    [Test]
    public void TestGetByNameAsync_Z()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("", 144, 1);
        
        //Act
        mockIngredientSet.Setup(I => I.GetByNameAsync("Cheese")).Throws(new Exception("Ingredient with name Cheese not found"));
        
        //Assert
        var e = Assert.ThrowsAsync<Exception>(() => ingredientLogic.GetByNameAsync("Cheese"));
        Assert.That(e.Message, Is.EqualTo("Ingredient with name Cheese not found"));
    }

    [Test]
    public void TestGetByNameAsync_O()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Cheese", 144, 1);
        
        //Act
        mockIngredientSet.Setup(I => I.GetByNameAsync("Cheese")).Returns(Task.FromResult(ingredient));
        
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.GetByNameAsync("Cheese"));
    }
}