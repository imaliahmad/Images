using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CategoryController : Controller
    {
        MyDbContext context = new MyDbContext();

        //Create Form, Edit Form
        public IActionResult Index()
        {
            //There are 2 ways to pass data from controller to view, view to controller
            //1. ViewBag, ViewData, TempData, Sessions
            //2 Model (C#)

            //Get CategoryList from database

          var objList =   context.Category.ToList();

            //Category obj1 = new Category() { CategoryId = , Name };
            //Category obj2 = new Category() { };
            //Category obj3 = new Category() { };

            //objList.Add(obj1);
            //objList.Add(obj2);
            //objList.Add(obj3);


            //objList.Add(new Category() { CategoryId = 1, Name = "Fruits"});
            //objList.Add(new Category() { CategoryId = 2, Name = "Food"});
            //objList.Add(new Category() { CategoryId = 3, Name = "Beverages" });

            ViewBag.CategoryList = objList;

            //ViewBag.CategoryList = context.Category.ToList();

            return View();
           
        }

        //Create --> id = 0
        //Edit --> id > 0
        [HttpGet]
        public IActionResult CreateOrEdit(int id) 
        {
            Category obj = new Category();

            if (id > 0)
            {
                obj = context.Category.Find(id);
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult CreateOrEdit(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model.CategoryId > 0) //Update/Edit/Modify
                {
                    context.Category.Update(model);
                    context.SaveChanges();
                }
                else //Insert/Add/Create
                {
                    context.Category.Add(model);
                    context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            Category obj = new Category();

            return View(obj); 
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                //add in database
                Category obj = new Category() { Name = model.Name };

                context.Category.Add(obj);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // get by id --> 
            var obj = context.Category.Find(id);

            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category model)
        {
            Category obj = new Category() {CategoryId = model.CategoryId, Name = model.Name };

            context.Category.Update(obj);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult GetCategorys()
        {
            var objList = context.Category.ToList();
            return Json(objList);
        }
    }
}
