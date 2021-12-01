using System;
using System.Collections.Generic;
using Catalog.Entities;
using System.Linq;

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
    public IEnumerable<Item> GetItems()
    {
      return items;
    }
    public Item GetItem(Guid id)
    {
      return items.Where(item => item.id == id).SingleOrDefault();
    }

    public void CreateItem(Item item)
    {
      items.Add(item);
    }

    public void UpdateItem(Item item)
    {
      var index = items.FindIndex(existingItem => existingItem.id == item.id);
      items[index] = item;
    }

    public void DeleteItem(Guid id)
    {
      var index = items.FindIndex(existingItem => existingItem.id == id);
      items.RemoveAt(index);
    }
  }
}