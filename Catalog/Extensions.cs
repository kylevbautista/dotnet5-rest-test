using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog{
    public static class Extensions{
        // The current item can have a method AsDto that returns its ItemDto version
        public static ItemDto AsDto(this Item item){
            return new ItemDto{
                id = item.id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }    
}