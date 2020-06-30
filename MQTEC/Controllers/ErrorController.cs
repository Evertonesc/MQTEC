using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MQTEC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ErrorTitle = "error";
            return View("Error");
        }
    }
}