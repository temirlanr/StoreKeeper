using StoreKeeper.Data;
using StoreKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreKeeper.Repositories
{
    public class StorageItemRepository : IStorageItemRepository
    {
        private readonly StoreKeeperContext context;

        public StorageItemRepository(StoreKeeperContext context)
        {
            this.context = context;
        }

        public void AddStorageItem(StorageItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.StorageItems.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<StorageItem> GetStorageItemsByStorageId(int storageId)
        {
            return context.StorageItems.Where(e => e.StorageId == storageId);
        }

        public StorageItem GetStorageItemById(int id)
        {
            return context.StorageItems.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<StorageItem> GetAllStorageItems()
        {
            return context.StorageItems.ToList();
        }

        public void UpdateStorageitem(StorageItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.StorageItems.Update(item);
            context.SaveChanges();
        }

        public void DeleteStorageItem(int id)
        {
            var item = context.StorageItems.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.StorageItems.Remove(item);
            context.SaveChanges();
        }
    }
}
