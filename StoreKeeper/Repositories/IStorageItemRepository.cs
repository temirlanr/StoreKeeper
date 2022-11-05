using StoreKeeper.Models;
using System.Collections.Generic;

namespace StoreKeeper.Repositories
{
    public interface IStorageItemRepository
    {
        void AddStorageItem(StorageItem item);
        IEnumerable<StorageItem> GetStorageItemsByStorageId(int storageId);
        StorageItem GetStorageItemById(int id);
        IEnumerable<StorageItem> GetAllStorageItems();
        void UpdateStorageitem(StorageItem item);
        void DeleteStorageItem(int id);
    }
}
