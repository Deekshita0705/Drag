using Drag.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Drag.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private static List<Item> table1 = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1" },
            new Item { Id = 2, Name = "Item 2" },
            new Item { Id = 3, Name = "Item 3" },
            new Item { Id = 4, Name = "Item 4" },
            new Item { Id = 5, Name = "Item 5" }
        };

        private static List<Item> table2 = new List<Item>();
        int lentable2 = table2.Count;

        public ActionResult Index()
        {
            ViewBag.Table1 = table1;
            ViewBag.Table2 = table2;
            return View();
        }

        [HttpPost]
        public ActionResult MoveForward(List<int> selectedItems)
        {
            if (table1.Count == 0)
            {
                 TempData["AlertMessage"] = "No items are present in Table 1.";
            }
            else if (selectedItems == null || selectedItems.Count == 0)
            {
                TempData["AlertMessage"] = "Select the items.";
            }
            else
            {
                foreach (var itemId in selectedItems)
                {
                    var item = table1.FirstOrDefault(i => i.Id == itemId);
                    if (item != null)
                    {
                        table1.Remove(item);
                        table2.Add(item);
                    }
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult MoveBackward(List<int> selectedItems)
        {
            if (table2.Count == 0)
            {
                
                TempData["AlertMessage"] = "No items are present in Table 2.";
            }
            else if (selectedItems == null || selectedItems.Count == 0)
            {
                
                TempData["AlertMessage"] = "Select the items.";
            }
            else
            {
                foreach (var itemId in selectedItems)
                {
                    var item = table2.FirstOrDefault(i => i.Id == itemId);
                    if (item != null)
                    {
                        table2.Remove(item);
                        table1.Add(item);
                    }

                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}