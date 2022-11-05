using StoreKeeper.Models;
using System.Collections.Generic;

namespace StoreKeeper.Services
{
    public interface ITransferService
    {
        void AddTransfer(Transfer transfer);
        Transfer GetTransfer(int id);
        IEnumerable<Transfer> GetTransfers();
        void UpdateTransfer(Transfer transfer);
        void DeleteTransfer(int id);
    }
}
