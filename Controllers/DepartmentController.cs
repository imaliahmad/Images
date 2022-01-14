using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DepartmentController : Controller
    {
        MyDbContext context = new MyDbContext();
        public IActionResult Index()
        {
            return View();
        }

        //CreateOrEdit
        public PartialViewResult GetDepartmentForm(int id)
        {
            Department obj = new Department();
            if (id > 0)
            {
                obj = context.Department.Find(id);
            }

            return PartialView(obj);
        }
        public PartialViewResult GetDepartmentList()
        {
            var objList = context.Department.ToList();
            return PartialView(objList);
        }

        public IActionResult Save(Department model)
        {
            if (model.DepartmentId > 0) //update
            {
                context.Department.Update(model);
                context.SaveChanges();

                return RedirectToAction(nameof(GetDepartmentList));
                //just testing
            }
            else //insert
            {
                context.Department.Add(model);
                context.SaveChanges();

                return RedirectToAction(nameof(GetDepartmentList));
            }
           
        }
    }
}
