using Microsoft.AspNetCore.Mvc;
using StoreKeeper.Models;
using StoreKeeper.Repositories;
using System;
using System.Collections.Generic;

namespace StoreKeeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            try
            {
                var items = itemRepository.GetAllItems();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{itemId}")]
        public ActionResult<Item> GetItem(int itemId)
        {
            try
            {
                var item = itemRepository.GetItemById(itemId);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Item> AddItem(Item item)
        {
            try
            {
                itemRepository.AddItem(item);
                return CreatedAtAction(nameof(GetItem), new { itemId = item.Id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{itemId}")]
        public ActionResult DeleteItem(int itemId)
        {
            try
            {
                itemRepository.DeleteItem(itemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
