using GroceryList.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryList.Database
{
    
    public class GroceryDatabaseContext : IGroceryDatabaseContext
    {
        private string _connectionString;

        public GroceryDatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
        public List<GroceryItem> GetAllGroceryItems()
        {
            List<GroceryItem> groceryItems = new List<GroceryItem>();

            MySqlConnection connection;
            MySqlCommand sqlCommand;
            MySqlDataReader dataReader;
            
            //connect db
            connection = GetConnection();
            connection.Open();

            //setup command
            sqlCommand = new MySqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM GROCERY_ITEM";

            dataReader = sqlCommand.ExecuteReader();

            //loop through results and populate list
            while (dataReader.Read())
            {
                GroceryItem groceryItem = new GroceryItem();

                groceryItem.Id = dataReader.GetInt32("Id");
                groceryItem.Name = dataReader.GetString("Name");

                groceryItem.Store = new Store();
                groceryItem.Store.Id = dataReader.GetInt32("StoreId");
                groceryItems.Add(groceryItem);
            }
            //close connection
            dataReader.Close();
            connection.Close();
            return groceryItems;
        }
        public List<Store> GetAllStores()
        {
            List<Store> stores = new List<Store>();

            MySqlConnection connection;
            MySqlCommand sqlCommand;
            MySqlDataReader dataReader;

            //connect db
            connection = GetConnection();
            connection.Open();

            //setup command
            sqlCommand = new MySqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM STORE";

            dataReader = sqlCommand.ExecuteReader();

            //loop through results and populate list
            while (dataReader.Read())
            {
                Store store = new Store();

                store.Id = dataReader.GetInt32("Id");
                store.Name = dataReader.GetString("Name");
                store.Address = dataReader.GetString("Address");
                store.City = dataReader.GetString("City");
                Console.WriteLine(store.City);
                store.State = dataReader.GetString("State");
                store.PhoneNumber = dataReader.GetString("PhoneNumber");
                stores.Add(store);
            }
            //close connection
            dataReader.Close();
            connection.Close();

            return stores;
        }
        public void AddGroceryItem(GroceryItem groceryItem)
        {
            MySqlConnection connection;
            MySqlCommand sqlCommand;

            //connect db
            connection = GetConnection();
            connection.Open();

            //setup command
            sqlCommand = new MySqlCommand();
            sqlCommand.Connection = connection;

            sqlCommand.CommandText = "INSERT INTO GROCERY_ITEM VALUES (NULL, @Name, @StoreId)";
            sqlCommand.Parameters.AddWithValue("@Name", groceryItem.Name);
            sqlCommand.Parameters.AddWithValue("@StoreId", groceryItem.Store.Id);
       

            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
        public void AddStore(Store store)
        {
            MySqlConnection connection;
            MySqlCommand sqlCommand;

            //connect db
            connection = GetConnection();
            connection.Open();

            //setup command
            sqlCommand = new MySqlCommand();
            sqlCommand.Connection = connection;

            sqlCommand.CommandText = "INSERT INTO STORE VALUES (NULL, @Name, @Address, @City, @State, @PhoneNumber)";

            sqlCommand.Parameters.AddWithValue("@Name", store.Name);
            sqlCommand.Parameters.AddWithValue("@Address", store.Address);
            sqlCommand.Parameters.AddWithValue("@City", store.City);
            sqlCommand.Parameters.AddWithValue("@State", store.State);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", store.PhoneNumber);
        
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}
