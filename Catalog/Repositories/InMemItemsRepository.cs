using System;
using System.Collections.Generic;
using Catalog.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Repositories{


  public class InMemItemsRespository : IItemsRespository
  {
    private readonly List<Item> items = new()
    {
      new Item
      {
        id = Guid.NewGuid(),
        Name = "Potion",
        Price = 9,
        CreatedDate = DateTimeOffset.UtcNow
      },
      new Item
      {
        id = Guid.NewGuid(),
        Name = "Iron Sword",
        Price = 20,
        CreatedDate = DateTimeOffset.UtcNow
      },
      new Item
      {
        id = Guid.NewGuid(),
        Name = "Bronze Shield",
        Price = 18,
        CreatedDate = DateTimeOffset.UtcNow
      },
    };
    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
      return await Task.FromResult(items);
    }
    public async Task<Item> GetItemAsync(Guid id)
    {
      var item = items.Where(item => item.id == id).SingleOrDefault();
      // Similar to Creating a promise and resolving it to item in say javascript
      return await Task.FromResult(item);
    }

    public async Task CreateItemAsync(Item item)
    {
      items.Add(item);
      // similar to creating and resolving promise fulfilled 
      await Task.CompletedTask;
    }

    public async Task UpdateItemAsync(Item item)
    {
      var index = items.FindIndex(existingItem => existingItem.id == item.id);
      items[index] = item;
      await Task.CompletedTask;
    }

    public async Task DeleteItemAsync(Guid id)
    {
      var index = items.FindIndex(existingItem => existingItem.id == id);
      items.RemoveAt(index);
      await Task.CompletedTask;
    }
  }
}