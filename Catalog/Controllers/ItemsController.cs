using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Controllers{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase{
        private readonly InMemItemsRespository respository;

        public ItemsController(){
            respository = new InMemItemsRespository();
        }

        // GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems(){
            var items = respository.GetItems();
            return items;
        }
    }
}