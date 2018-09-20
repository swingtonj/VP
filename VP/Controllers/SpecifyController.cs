using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VP.Controllers
{
    public class SpecifyController : Controller
    {
        // GET: Specify
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Result()
        {
            return RedirectToAction("Index", "Specify", new { input = "#step-5" });
        }
    }
}