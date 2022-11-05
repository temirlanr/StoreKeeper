using StoreKeeper.Models;
using System.Collections.Generic;

namespace StoreKeeper.Repositories
{
    public interface ITransferRepository
    {
        void AddTransfer(Transfer transfer);
        Transfer GetTransferById(int id);
        IEnumerable<Transfer> GetAllTransfers();
        void UpdateTransfer(Transfer transfer);
        void DeleteTransfer(int id);
    }
}
