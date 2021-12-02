using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System.Linq;
using Catalog.Dtos;
using System.Threading.Tasks;

namespace Catalog.Controllers{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase{
        private readonly IItemsRespository respository;

        public ItemsController(IItemsRespository respository){
            this.respository = respository;
        }

        // GET /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync(){

            // have to await the getItemsasync then select the correct item
            var items = (await respository.GetItemsAsync())
                        .Select(item=> item.AsDto());
            return items;
        }
        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id){
            var item = await respository.GetItemAsync(id);

            if(item is null){
                return NotFound();
            }
            return item.AsDto();
        }

        // POST /items
        // usually returns item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto){
            Item item = new(){
                id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await respository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new{id = item.id},item.AsDto());
        }

        // PUT /items/{id}
        //
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto){
            var existingItem = await respository.GetItemAsync(id);
            if(existingItem is null){
                return NotFound();
            }
            // with expression explicit to record types
            // with makes a copy of existing item with defined modified properties
            Item updatedItem = existingItem with{
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            await respository.UpdateItemAsync(updatedItem);
            return NoContent();
        }

        // DELETE /item/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id){
            var existingItem = await respository.GetItemAsync(id);
            if(existingItem is null){
                return NotFound();
            }
            await respository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}