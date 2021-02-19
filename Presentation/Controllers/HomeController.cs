using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Infrastructure;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var list1 = get();
            ViewBag.List1 = list1;

            var list2 = get();

            return View(list2);
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
        private List<TreeViewModel> get()
        {
            var list = new List<TreeViewModel>() {
                new TreeViewModel() { Id = "1", Text = "one", ParentId =null },
                new TreeViewModel() { Id = "2", Text = "two", ParentId =null },
                new TreeViewModel() { Id = "3", Text = "three", ParentId =null },
                new TreeViewModel() { Id = "4", Text = "four", ParentId ="1" },
                new TreeViewModel() { Id = "5", Text = "five", ParentId ="1" },
                new TreeViewModel() { Id = "6", Text = "six", ParentId ="1" },
                new TreeViewModel() { Id = "7", Text = "seven", ParentId ="4" },
                new TreeViewModel() { Id = "8", Text = "eight", ParentId ="5" },
                new TreeViewModel() { Id = "9", Text = "nine", ParentId ="5" },
                new TreeViewModel() { Id = "10", Text = "ten", ParentId ="4" },
                new TreeViewModel() { Id = "11", Text = "eleven", ParentId ="7" },
                new TreeViewModel() { Id = "12", Text = "twelve", ParentId ="11" },
                new TreeViewModel() { Id = "13", Text = "thirteen", ParentId ="4" },
                new TreeViewModel() { Id = "14", Text = "fourteen", ParentId ="2" },
                new TreeViewModel() { Id = "15", Text = "fifteen", ParentId ="2" },
                new TreeViewModel() { Id = "16", Text = "sixteen", ParentId ="2" }
            };
            return list;
        }

    }
}
