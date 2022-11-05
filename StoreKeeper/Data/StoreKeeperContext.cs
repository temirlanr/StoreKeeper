using Microsoft.EntityFrameworkCore;
using StoreKeeper.Models;

namespace StoreKeeper.Data
{
    public class StoreKeeperContext : DbContext
    {
        public StoreKeeperContext(DbContextOptions<StoreKeeperContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferItem> TransferItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>().HasMany(storage => storage.StorageItems).WithOne(item => item.Storage).HasForeignKey(e => e.StorageId);
            modelBuilder.Entity<Transfer>().HasMany(transfer => transfer.TransferItems).WithOne(item => item.Transfer).HasForeignKey(e => e.TransferId);
            modelBuilder.Entity<Item>().HasMany(item => item.TransferItems).WithOne(item => item.Item).HasForeignKey(e => e.ItemId);
            modelBuilder.Entity<Item>().HasMany(item => item.StorageItems).WithOne(item => item.Item).HasForeignKey(e => e.ItemId);
        }
    }
}
