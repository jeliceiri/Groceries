using System.Collections.Generic;
using GroceryList.Models;
using MySql.Data.MySqlClient;

namespace GroceryList.Database
{
    public interface IGroceryDatabaseContext
    {
        void AddGroceryItem(GroceryItem groceryItem);
        void AddStore(Store store);
        List<GroceryItem> GetAllGroceryItems();
        List<Store> GetAllStores(); 
    }
}