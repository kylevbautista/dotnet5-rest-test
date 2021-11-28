using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;

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
        public IEnumerable<Item> GetItems(){
            var items = respository.GetItems();
            return items;
        }
        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id){
            var item = respository.GetItem(id);

            if(item is null){
                return NotFound();
            }
            return item;
        }
    }
}