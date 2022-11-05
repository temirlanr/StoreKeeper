using StoreKeeper.Models;
using StoreKeeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreKeeper.Services
{
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository storageRepository;
        private readonly IStorageItemRepository storageItemRepository;
        private readonly IItemRepository itemRepository;

        public StorageService(IStorageRepository storageRepository, IStorageItemRepository storageItemRepository, IItemRepository itemRepository)
        {
            this.storageRepository = storageRepository;
            this.storageItemRepository = storageItemRepository;
            this.itemRepository = itemRepository;
        }

        public void AddStorage(Storage storage)
        {
            storageRepository.AddStorage(storage);
        }

        public Storage GetStorage(int id)
        {
            var storage = storageRepository.GetStorageById(id);

            if(storage == null)
            {
                throw new ArgumentException($"Storage with id: {id} not found.");
            }

            storage.StorageItems = storageItemRepository.GetStorageItemsByStorageId(id).ToList();

            foreach(var storageItem in storage.StorageItems)
            {
                storageItem.Item = itemRepository.GetItemById(storageItem.ItemId);
            }

            return storage;
        }

        public IEnumerable<Storage> GetStorages()
        {
            var storages = storageRepository.GetAllStorages();

            foreach(var storage in storages)
            {
                storage.StorageItems = storageItemRepository.GetStorageItemsByStorageId(storage.Id).ToList();

                foreach (var storageItem in storage.StorageItems)
                {
                    storageItem.Item = itemRepository.GetItemById(storageItem.ItemId);
                }
            }

            return storages;
        }

        public void UpdateStorage(Storage storage)
        {
            storageRepository.UpdateStorage(storage);
        }

        public void DeleteStorage(int id)
        {
            var storage = storageRepository.GetStorageById(id);

            if (storage == null)
            {
                throw new ArgumentException("Not Found.");
            }

            foreach(var storageItem in storage.StorageItems)
            {
                storageItemRepository.DeleteStorageItem(storageItem.Id);
            }

            storageRepository.DeleteStorage(id);
        }
    }
}
