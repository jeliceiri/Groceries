using GroceryList.Database;
using GroceryList.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryList.Controllers
{
    public class GroceriesController : Controller
    {
        private IGroceryDatabaseContext _databaseContext;

        public GroceriesController(IGroceryDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IActionResult Index()
        {
            List<GroceryItem> groceryItems = _databaseContext.GetAllGroceryItems();
            return View(groceryItems);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AvailableStores = _databaseContext.GetAllStores();
            return View();
        }
        [HttpPost]

        public IActionResult Create (GroceryItem groceryItem)
        {

            if (ModelState.IsValid == true)
            {

                _databaseContext.AddGroceryItem(groceryItem);
                return RedirectToAction("Index");
            }
            return View(groceryItem);
        }
    }
}
