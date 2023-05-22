using Application.DaoInterfaces;
using Application.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Moq.Language.Flow;
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

    [Test]
    public void TestAddIngredient_Z()
    {
        //Arrange
        Ingredient creation = new Ingredient("", 55, 1);
        IngredientCreationDto creationDto = new IngredientCreationDto("", 55, 1);
        //Act
        mockIngredientSet.Setup(I => I.GetByNameAsync("")).Returns(Task.FromResult(creation)!);
        mockIngredientSet.Setup(I => I.CreateAsync(creation)).Returns(Task.FromResult(creation));
        //Assert
        Assert.ThrowsAsync<Exception>(()=> ingredientLogic.CreateAsync(creationDto));
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
    public void TestAddIngredient_M()
    {
        //Arrange
        Ingredient creation = new Ingredient("Milk", 55, 1);
        Ingredient creation2 = new Ingredient("Cheese", 100, 1);
        IngredientCreationDto creationDto = new IngredientCreationDto("Milk", 55, 1);
        IngredientCreationDto creationDto2 = new IngredientCreationDto("Cheese", 100, 1);
        //Act
        mockIngredientSet.Setup(I => I.CreateAsync(creation)).Returns(Task.FromResult(creation));
        mockIngredientSet.Setup(I => I.CreateAsync(creation2)).Returns(Task.FromResult(creation2));
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.CreateAsync(creationDto));
        Assert.DoesNotThrowAsync(() => ingredientLogic.CreateAsync(creationDto2));
    }

    [Test]
    public void TestRemoveIngredient_Z()
    {
        //Arrange

        //Act
        mockIngredientSet.Setup(I => I.DeleteAsync(1));
        //Assert
        Assert.ThrowsAsync<Exception>(() => ingredientLogic.DeleteIngredient(1));
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
    
    [Test]
    public void TestRemoveIngredient_M()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Potato", 100, 0);
        Ingredient ingredient1 = new Ingredient("Bread", 300, 6);
        //Act
        mockIngredientSet.Setup(I => I.GetByIdAsync(1)).Returns(Task.FromResult(ingredient1));
        mockIngredientSet.Setup(I => I.GetByIdAsync(0)).Returns(Task.FromResult(ingredient));
        mockIngredientSet.Setup(I => I.DeleteAsync(0));
        mockIngredientSet.Setup(I => I.DeleteAsync(1));
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.DeleteIngredient(0));
        Assert.DoesNotThrowAsync(() => ingredientLogic.DeleteIngredient(1));
    }

    [Test]
    public void TestUpdateIngredient_Z()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("", 0, 0);
        IngredientUpdateDto updateDto = new IngredientUpdateDto(1, 100);
        //Act
        mockIngredientSet.Setup(I => I.UpdateAsync(ingredient)).Returns(Task.FromResult(ingredient));
        //Assert
        Assert.ThrowsAsync<Exception>(() => ingredientLogic.UpdateIngredientAmount(updateDto));
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
    
    [Test]
    public void TestGetByIdAsync_M()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Cheese", 144, 1);
        Ingredient ingredient1 = new Ingredient("Milk", 47, 1);
        Ingredient ingredient2 = new Ingredient("Yogurt", 174, 1);
        
        //Act
        mockIngredientSet.Setup(I => I.GetByIdAsync(ingredient.Id)).Returns(Task.FromResult(ingredient));
        mockIngredientSet.Setup(I => I.GetByIdAsync(ingredient1.Id)).Returns(Task.FromResult(ingredient));
        mockIngredientSet.Setup(I => I.GetByIdAsync(ingredient2.Id)).Returns(Task.FromResult(ingredient));
        
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.GetByIdAsync(ingredient.Id));
        Assert.DoesNotThrowAsync(() => ingredientLogic.GetByIdAsync(ingredient1.Id));
        Assert.DoesNotThrowAsync(() => ingredientLogic.GetByIdAsync(ingredient2.Id));
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
    
    [Test]
    public void TestGetByNameAsync_M()
    {
        //Arrange
        Ingredient ingredient = new Ingredient("Cheese", 144, 1);
        Ingredient ingredient1 = new Ingredient("Milk", 47, 1);
        Ingredient ingredient2 = new Ingredient("Yogurt", 174, 1);
        
        //Act
        mockIngredientSet.Setup(I => I.GetByNameAsync("Cheese")).Returns(Task.FromResult(ingredient));
        mockIngredientSet.Setup(I => I.GetByNameAsync("Milk")).Returns(Task.FromResult(ingredient1));
        mockIngredientSet.Setup(I => I.GetByNameAsync("Yogurt")).Returns(Task.FromResult(ingredient2));
        
        //Assert
        Assert.DoesNotThrowAsync(() => ingredientLogic.GetByNameAsync("Cheese"));
        Assert.DoesNotThrowAsync(() => ingredientLogic.GetByNameAsync("Milk"));
        Assert.DoesNotThrowAsync(() => ingredientLogic.GetByNameAsync("Yogurt"));
    } 
}