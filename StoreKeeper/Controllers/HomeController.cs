using Microsoft.AspNetCore.Mvc;
using StoreKeeper.Models;
using StoreKeeper.Repositories;
using System;
using System.Collections.Generic;

namespace StoreKeeper.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IStorageItemRepository _storageItemRepository;
        private readonly IItemRepository _itemRepository;

        public HomeController(IStorageRepository storageRepository, IStorageItemRepository storageItemRepository, IItemRepository itemRepository)
        {
            _storageRepository = storageRepository;
            _storageItemRepository = storageItemRepository;
            _itemRepository = itemRepository;
        }

        private readonly List<Item> initialItems = new List<Item>
        {
            new Item() { Name = "Chair" },
            new Item() { Name = "Wheel" },
            new Item() { Name = "Tyre" },
            new Item() { Name = "Sofa" },
            new Item() { Name = "Bottle" },
            new Item() { Name = "Laptop" },
            new Item() { Name = "Phone" }
        };

        private readonly List<Storage> initialStorages = new List<Storage>
        {
            new Storage() { Name = "Almaty" },
            new Storage() { Name = "Astana" },
            new Storage() { Name = "Shymkent" }
        };

        [HttpGet]
        [Route("api/initialize")]
        public ActionResult Initialize()
        {
            try
            {
                foreach (var item in initialItems)
                {
                    _itemRepository.AddItem(item);
                }

                foreach (var storage in initialStorages)
                {
                    _storageRepository.AddStorage(storage);
                }

                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[0], Quantity = 10, Storage = initialStorages[1] });
                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[1], Quantity = 15, Storage = initialStorages[0] });
                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[2], Quantity = 13, Storage = initialStorages[0] });
                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[3], Quantity = 16, Storage = initialStorages[1] });
                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[4], Quantity = 178, Storage = initialStorages[0] });
                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[5], Quantity = 11, Storage = initialStorages[1] });
                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[5], Quantity = 113, Storage = initialStorages[2] });
                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[6], Quantity = 112, Storage = initialStorages[2] });
                _storageItemRepository.AddStorageItem(new StorageItem() { Item = initialItems[2], Quantity = 1, Storage = initialStorages[0] });

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
