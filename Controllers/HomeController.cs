﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        //html,css, bootstrap, js

        //manually
        //client-side
        public IActionResult Index()
        {
            return View();
        }
    }
}
