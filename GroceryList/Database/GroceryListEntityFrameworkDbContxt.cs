using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryList.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryList.Database
{
    public class GroceryListEntityFrameworkDbContxt : DbContext, IGroceryDatabaseContext
    {
        public GroceryListEntityFrameworkDbContxt(DbContextOptions<GroceryListEntityFrameworkDbContxt> options)
            : base(options)
        {

        }

        DbSet<GroceryItem> GroceryItems { get; set; }

        DbSet<Store> Stores { get; set; }

        public void AddGroceryItem(GroceryItem groceryItem)
        {
            GroceryItems.Add(groceryItem);
            SaveChanges();
        }

        public void AddStore(Store store)
        {
            Stores.Add(store);
            SaveChanges();
        }

        public List<GroceryItem> GetAllGroceryItems()
        {
            return GroceryItems
                .Include(groceryItem => groceryItem.Store)
                .ToList();
        }

        public List<Store> GetAllStores()
        {
            return Stores.ToList();
        }
    }
}
