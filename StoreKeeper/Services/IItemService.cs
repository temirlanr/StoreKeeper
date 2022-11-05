using StoreKeeper.Models;
using System.Collections.Generic;

namespace StoreKeeper.Services
{
    public interface IItemService
    {
        void AddItem(Item item);
        Item GetItem(int id);
        IEnumerable<Item> GetItems();
        void UpdateItem(Item item);
        void DeleteItem(int id);
    }
}
