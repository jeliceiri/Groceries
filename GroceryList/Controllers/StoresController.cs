using GroceryList.Database;
using GroceryList.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryList.Controllers
{
    public class StoresController : Controller 
    {
        private IGroceryDatabaseContext _databaseContext;

        public StoresController(IGroceryDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IActionResult Index()
        {
            List<Store> storeList = _databaseContext.GetAllStores();

            return View(storeList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AvailableStores = _databaseContext.GetAllStores();
            return View();
        }
        [HttpPost]

        public IActionResult Create(Store store)
        {
            if (ModelState.IsValid == true)
            {
                _databaseContext.AddStore(store);
                return RedirectToAction("Index");
            }
            return View(store);
        }
    }
}
