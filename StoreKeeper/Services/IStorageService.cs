using StoreKeeper.Models;
using System.Collections.Generic;

namespace StoreKeeper.Services
{
    public interface IStorageService
    {
        void AddStorage(Storage storage);
        Storage GetStorage(int id);
        IEnumerable<Storage> GetStorages();
        void UpdateStorage(Storage storage);
        void DeleteStorage(int id);
    }
}
