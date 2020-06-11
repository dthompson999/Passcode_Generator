using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            string Characters = "ABCDEFG1234567890";
            Random rand = new Random();
            string Passcode = "";
            for (int i=1; i<=13; i++)
            {
                Passcode += Characters[rand.Next(Characters.Length)];
            }
            ViewBag.Passcode = Passcode;
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 0);
            }
            int? count = HttpContext.Session.GetInt32("Count");
            count ++;
            HttpContext.Session.SetInt32("Count", (int)count);
            ViewBag.Count = HttpContext.Session.GetInt32("Count");
            return View();
        }

        [HttpGet("reset")]
        public IActionResult Clear()
        {
            string Characters = "ABCDEFG1234567890";
            Random rand = new Random();
            string Passcode = "";
            for (int i=0; i<=13; i++)
            {
                Passcode += Characters[rand.Next(Characters.Length)];
            }
            ViewBag.Passcode = Passcode;
            HttpContext.Session.Clear();
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 0);
            }
            int? count = HttpContext.Session.GetInt32("Count");
            count ++;
            HttpContext.Session.SetInt32("Count", (int)count);
            ViewBag.Count = HttpContext.Session.GetInt32("Count");
            return View("Index");
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
