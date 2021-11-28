using System;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories{

    public class InMemItemsRespository{
        private readonly List<Item> items = new(){
            new Item{ 
                ID=Guid.NewGuid(), 
                Name = "Potion", 
                Price = 9, 
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Item{ 
                ID=Guid.NewGuid(), 
                Name = "Iron Sword", 
                Price = 20, 
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Item{ 
                ID = Guid.NewGuid(), 
                Name = "Bronze Shield", 
                Price = 18, 
                CreatedDate = DateTimeOffset.UtcNow
            },
        };
    }
}