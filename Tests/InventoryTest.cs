using Xunit; 
using Quinquennium.Scripts.Player.Inventory;
using Quinquennium.Scripts.Resources;

namespace Quinquennium.Tests;

public class InventoryTests
{
    [Fact]
    public void AddItem_ShouldCreateNewStack_WhenInventoryIsEmpty()
    {
        // 1. ARRANGE (Подготовка)
        var inventory = new Inventory();
        
        var mockItem = new ItemResource();
        mockItem.Id = "wood";
        mockItem.MaxCount = 30;

        inventory.AddItem(mockItem, 10);

        var items = inventory.GetItems(); 
        
        Assert.Single(items); // Проверка: в списке ровно один элемент
        Assert.Equal(10, items[0].Amount); // Проверка: количество совпадает
        Assert.Equal("wood", items[0].ItemResource.Id); // Проверка: это то самое дерево
    }
    
    [Theory]
    [InlineData(10, 1)] // Тест 1: добавили 10, ждем 1 стак
    [InlineData(30, 1)] // Тест 2: добавили 30, ждем 1 стак
    [InlineData(45, 2)] // Тест 3: добавили 45, ждем 2 стака
    public void AddItem_ShouldCreateCorrectAmountOfStacks(int countToAdd, int expectedStacksCount)
    {
        // Arrange
        var inventory = new Inventory();
        var mockItem = new ItemResource { Id = "iron", MaxCount = 30 };

        // Act
        inventory.AddItem(mockItem, countToAdd);

        // Assert
        var items = inventory.GetItems();
        Assert.Equal(expectedStacksCount, items.Count);
    }
}