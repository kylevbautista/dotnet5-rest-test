using System;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories{
    // A depency and maybe this is a service as well? the buisness logic?
    public interface IItemsRespository{
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void CreateItem(Item item);
        void UpdateItem(Item item);
    }
}