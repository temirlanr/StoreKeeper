using StoreKeeper.Models;
using StoreKeeper.Repositories;
using System;
using System.Collections.Generic;

namespace StoreKeeper.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public void AddItem(Item item)
        {
            itemRepository.AddItem(item);
        }

        public Item GetItem(int id)
        {
            return itemRepository.GetItemById(id);
        }

        public IEnumerable<Item> GetItems()
        {
            return itemRepository.GetAllItems();
        }

        public void UpdateItem(Item item)
        {
            itemRepository.UpdateItem(item);
        }

        public void DeleteItem(int id)
        {
            var item = itemRepository.GetItemById(id);

            if(item == null)
            {
                throw new ArgumentException("Not Found.");
            }

            itemRepository.DeleteItem(id);
        }
    }
}
