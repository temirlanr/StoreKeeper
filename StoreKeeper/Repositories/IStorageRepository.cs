using StoreKeeper.Models;
using System.Collections.Generic;

namespace StoreKeeper.Repositories
{
    public interface IStorageRepository
    {
        void AddStorage(Storage storage);
        Storage GetStorageById(int id);
        IEnumerable<Storage> GetAllStorages();
        void UpdateStorage(Storage storage);
        void DeleteStorage(int id);
    }
}
