﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStore.Web.Models;

namespace MyStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        
        public IActionResult Contact()
        {
            throw new Exception("oopss...");
            
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpGet("date")]
        [ResponseCache(Duration = 10, VaryByHeader = "X-Custom", VaryByQueryKeys = new []{"Q"})]
        public IActionResult GetDate()
            => Content(DateTime.Now.ToString());

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
