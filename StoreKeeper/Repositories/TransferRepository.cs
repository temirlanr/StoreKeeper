using StoreKeeper.Data;
using StoreKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreKeeper.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly StoreKeeperContext context;

        public TransferRepository(StoreKeeperContext context)
        {
            this.context = context;
        }

        public void AddTransfer(Transfer transfer)
        {
            if (transfer == null)
            {
                throw new ArgumentNullException(nameof(transfer));
            }

            context.Transfers.Add(transfer);
            context.SaveChanges();
        }

        public Transfer GetTransferById(int id)
        {
            return context.Transfers.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Transfer> GetAllTransfers()
        {
            return context.Transfers.ToList();
        }

        public void UpdateTransfer(Transfer transfer)
        {
            if (transfer == null)
            {
                throw new ArgumentNullException(nameof(transfer));
            }

            context.Transfers.Update(transfer);
            context.SaveChanges();
        }

        public void DeleteTransfer(int id)
        {
            var transfer = context.Transfers.FirstOrDefault(e => e.Id == id);

            if (transfer == null)
            {
                throw new ArgumentNullException(nameof(transfer));
            }

            context.Transfers.Remove(transfer);
            context.SaveChanges();
        }
    }
}
