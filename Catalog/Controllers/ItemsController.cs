using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System.Linq;
using Catalog.Dtos;

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
        public IEnumerable<ItemDto> GetItems(){
            var items = respository.GetItems().Select(item=> item.AsDto());
            return items;
        }
        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id){
            var item = respository.GetItem(id);

            if(item is null){
                return NotFound();
            }
            return item.AsDto();
        }

        // POST /items
        // usually returns item
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto){
            Item item = new(){
                ID = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            respository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new{id = item.ID},item.AsDto());
        }

        // PUT /items/{id}
        //
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto){
            var existingItem = respository.GetItem(id);
            if(existingItem is null){
                return NotFound();
            }
            // with expression explicit to record types
            // with makes a copy of existing item with defined modified properties
            Item updatedItem = existingItem with{
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            respository.UpdateItem(updatedItem);
            return NoContent();
        }
    }
}