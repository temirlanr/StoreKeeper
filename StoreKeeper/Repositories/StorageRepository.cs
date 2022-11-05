using StoreKeeper.Data;
using StoreKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreKeeper.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly StoreKeeperContext context;

        public StorageRepository(StoreKeeperContext context)
        {
            this.context = context;
        }

        public void AddStorage(Storage storage)
        {
            if(storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            context.Storages.Add(storage);
            context.SaveChanges();
        }

        public Storage GetStorageById(int id)
        {
            return context.Storages.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Storage> GetAllStorages()
        {
            return context.Storages.ToList();
        }

        public void UpdateStorage(Storage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            context.Storages.Update(storage);
            context.SaveChanges();
        }

        public void DeleteStorage(int id)
        {
            var storage = context.Storages.FirstOrDefault(e => e.Id == id);

            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            context.Storages.Remove(storage);
            context.SaveChanges();
        }
    }
}
