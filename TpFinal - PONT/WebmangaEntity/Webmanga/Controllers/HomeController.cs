using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webmanga.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["id"] = null;
            Session["role"] = null;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NouvUtil()
        {
            return View();
        }
    }
}