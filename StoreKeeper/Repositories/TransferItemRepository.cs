using StoreKeeper.Data;
using StoreKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreKeeper.Repositories
{
    public class TransferItemRepository : ITransferItemRepository
    {
        private readonly StoreKeeperContext context;

        public TransferItemRepository(StoreKeeperContext context)
        {
            this.context = context;
        }

        public void AddTransferItem(TransferItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.TransferItems.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<TransferItem> GetTransferItemsByTransferId(int transferId)
        {
            return context.TransferItems.Where(x => x.TransferId == transferId);
        }

        public TransferItem GetTransferItemById(int id)
        {
            return context.TransferItems.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<TransferItem> GetAllTransferItems()
        {
            return context.TransferItems.ToList();
        }

        public void UpdateTransferItem(TransferItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.TransferItems.Update(item);
            context.SaveChanges();
        }

        public void DeleteTransferItem(int id)
        {
            var item = context.TransferItems.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            context.TransferItems.Remove(item);
            context.SaveChanges();
        }
    }
}
