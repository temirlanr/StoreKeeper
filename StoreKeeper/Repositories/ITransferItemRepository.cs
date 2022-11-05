using StoreKeeper.Models;
using System.Collections.Generic;

namespace StoreKeeper.Repositories
{
    public interface ITransferItemRepository
    {
        void AddTransferItem(TransferItem item);
        IEnumerable<TransferItem> GetTransferItemsByTransferId(int transferId);
        TransferItem GetTransferItemById(int id);
        IEnumerable<TransferItem> GetAllTransferItems();
        void UpdateTransferItem(TransferItem item);
        void DeleteTransferItem(int id);
    }
}
