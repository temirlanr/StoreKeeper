using StoreKeeper.Data;
using StoreKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreKeeper.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly StoreKeeperContext context;

        public ItemRepository(StoreKeeperContext context)
        {
            this.context = context;
        }

        public void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.Items.Add(item);
            context.SaveChanges();
        }

        public Item GetItemById(int id)
        {
            return context.Items.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return context.Items.ToList();
        }

        public void UpdateItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.Items.Update(item);
            context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var item = context.Items.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.Items.Remove(item);
            context.SaveChanges();
        }
    }
}
