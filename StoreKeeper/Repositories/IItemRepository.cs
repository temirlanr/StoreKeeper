using StoreKeeper.Models;
using System.Collections.Generic;

namespace StoreKeeper.Repositories
{
    public interface IItemRepository
    {
        void AddItem(Item item);
        Item GetItemById(int id);
        IEnumerable<Item> GetAllItems();
        void UpdateItem(Item item);
        void DeleteItem(int id);
    }
}
