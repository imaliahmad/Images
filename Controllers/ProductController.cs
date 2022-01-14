using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProductController : Controller
    {
        MyDbContext context = new MyDbContext();
        public IActionResult Index()
        {
            List<Product> objList = context.Product.ToList();
            return View(objList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryList= context.Category.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                context.Product.Add(model);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
