using StoreKeeper.Models;
using StoreKeeper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreKeeper.Services
{
    public class TransferService : ITransferService
    {
        private readonly IStorageRepository storageRepository;
        private readonly IStorageItemRepository storageItemRepository;
        private readonly ITransferRepository transferRepository;
        private readonly ITransferItemRepository transferItemRepository;
        private readonly IItemRepository itemRepository;

        public TransferService(IStorageRepository storageRepository, ITransferRepository transferRepository, ITransferItemRepository transferItemRepository, IStorageItemRepository storageItemRepository, IItemRepository itemRepository)
        {
            this.storageRepository = storageRepository;
            this.transferRepository = transferRepository;
            this.transferItemRepository = transferItemRepository;
            this.storageItemRepository = storageItemRepository;
            this.itemRepository = itemRepository;
        }

        public void AddTransfer(Transfer transfer)
        {
            var counter = new List<int>();

            foreach (var item in transfer.TransferItems)
            {
                if (counter.Contains(item.ItemId))
                {
                    throw new ArgumentException("Items must be unique in a transfer.");
                }
                else
                {
                    counter.Add(item.ItemId);
                }
            }

            var fromStorageItem = storageItemRepository.GetStorageItemsByStorageId(transfer.FromStorageId).ToList();

            if (fromStorageItem.Count == 0)
            {
                throw new ArgumentException($"Storage items of storage id {transfer.FromStorageId} not found.");
            }

            var toStorageItem = storageItemRepository.GetStorageItemsByStorageId(transfer.ToStorageId).ToList();

            if (toStorageItem.Count == 0)
            {
                throw new ArgumentException($"Storage item of storage id {transfer.ToStorageId} not found.");
            }

            StorageItem fromStorageItemToUpdate = new StorageItem();
            StorageItem toStorageItemToUpdate = new StorageItem();

            foreach (var transferItem in transfer.TransferItems)
            {
                var item = transferItem.Item;
                
                if (fromStorageItem.Select(e => e.Item).Contains(item))
                {
                    fromStorageItemToUpdate = fromStorageItem[fromStorageItem.Select(e => e.Item).ToList().IndexOf(item)];
                    fromStorageItemToUpdate.Quantity -= transferItem.Quantity;
                    
                    if(fromStorageItemToUpdate.Quantity < 0)
                    {
                        throw new ArgumentException($"There not enough items with id: {item.Id} in storage with id: {transfer.FromStorageId}");
                    }
                }
                else
                {
                    throw new ArgumentException($"Storage with id: {transfer.FromStorageId} does not contain item with id: {item.Id}");
                }

                if(toStorageItem.Select(e => e.Item).Contains(item))
                {
                    toStorageItemToUpdate = toStorageItem[toStorageItem.Select(e => e.Item).ToList().IndexOf(item)];
                    toStorageItemToUpdate.Quantity += transferItem.Quantity;
                }
                else
                {
                    var storageItem = new StorageItem();
                    storageItem.StorageId = transfer.ToStorageId;
                    storageItem.Quantity = transferItem.Quantity;
                    storageItem.ItemId = item.Id;

                    storageItemRepository.AddStorageItem(storageItem);
                }
            }

            storageItemRepository.UpdateStorageitem(fromStorageItemToUpdate);
            storageItemRepository.UpdateStorageitem(toStorageItemToUpdate);
            transferRepository.AddTransfer(transfer);
        }

        public Transfer GetTransfer(int id)
        {
            var transfer = transferRepository.GetTransferById(id);

            if(transfer == null)
            {
                throw new ArgumentException($"Transfer with id {id} not found.");
            }

            transfer.FromStorage = storageRepository.GetStorageById(transfer.FromStorageId);
            transfer.ToStorage = storageRepository.GetStorageById(transfer.ToStorageId);

            transfer.TransferItems = transferItemRepository.GetTransferItemsByTransferId(id).ToList();

            foreach(var transferItem in transfer.TransferItems)
            {
                transferItem.Item = itemRepository.GetItemById(transferItem.ItemId);
            }

            return transfer;
        }

        public IEnumerable<Transfer> GetTransfers()
        {
            var transfers = transferRepository.GetAllTransfers();

            foreach(var transfer in transfers)
            {
                transfer.FromStorage = storageRepository.GetStorageById(transfer.FromStorageId);
                transfer.ToStorage = storageRepository.GetStorageById(transfer.ToStorageId);

                transfer.TransferItems = transferItemRepository.GetTransferItemsByTransferId(transfer.Id).ToList();

                foreach(var transferItem in transfer.TransferItems)
                {
                    transferItem.Item = itemRepository.GetItemById(transferItem.ItemId);
                }
            }

            return transfers;
        }

        public void UpdateTransfer(Transfer transfer)
        {
            transferRepository.UpdateTransfer(transfer);
        }

        public void DeleteTransfer(int id)
        {
            var transfer = transferRepository.GetTransferById(id);

            if (transfer == null)
            {
                throw new ArgumentException("Not Found.");
            }

            if (transfer.TransferItems != null)
            {
                var fromStorageItem = storageItemRepository.GetStorageItemsByStorageId(transfer.FromStorageId).ToList();

                if (fromStorageItem.Count == 0)
                {
                    throw new ArgumentException($"Storage items of storage id {transfer.FromStorageId} not found.");
                }

                var toStorageItem = storageItemRepository.GetStorageItemsByStorageId(transfer.ToStorageId).ToList();

                if (toStorageItem.Count == 0)
                {
                    throw new ArgumentException($"Storage item of storage id {transfer.ToStorageId} not found.");
                }

                StorageItem fromStorageItemToUpdate = new StorageItem();
                StorageItem toStorageItemToUpdate = new StorageItem();

                foreach (var transferItem in transfer.TransferItems)
                {
                    var item = transferItem.Item;

                    if (fromStorageItem.Select(e => e.Item).Contains(item))
                    {
                        fromStorageItemToUpdate = fromStorageItem[fromStorageItem.Select(e => e.Item).ToList().IndexOf(item)];
                        fromStorageItemToUpdate.Quantity += transferItem.Quantity;
                    }
                    else
                    {
                        var storageItem = new StorageItem();
                        storageItem.StorageId = transfer.FromStorageId;
                        storageItem.Quantity = transferItem.Quantity;
                        storageItem.ItemId = item.Id;

                        storageItemRepository.AddStorageItem(storageItem);
                    }

                    if (toStorageItem.Select(e => e.Item).Contains(item))
                    {
                        toStorageItemToUpdate = toStorageItem[toStorageItem.Select(e => e.Item).ToList().IndexOf(item)];
                        toStorageItemToUpdate.Quantity -= transferItem.Quantity;

                        if (toStorageItemToUpdate.Quantity < 0)
                        {
                            throw new ArgumentException($"There not enough items with id: {item.Id} in storage with id: {transfer.FromStorageId}");
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Storage with id: {transfer.ToStorageId} does not contain item with id: {item.Id}");
                    }

                    storageItemRepository.UpdateStorageitem(fromStorageItemToUpdate);
                    storageItemRepository.UpdateStorageitem(toStorageItemToUpdate);
                    transferItemRepository.DeleteTransferItem(transferItem.Id);
                }
            }

            transferRepository.DeleteTransfer(id);
        }
    }
}
