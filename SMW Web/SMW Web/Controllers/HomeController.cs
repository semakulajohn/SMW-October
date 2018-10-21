using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMW_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Property()
        {
            ViewBag.Message = "Properties";

            return View();
        }
        public ActionResult OurServices()
        {
            ViewBag.Message = "Services";

            return View();
        }
        public ActionResult ServiceDetail()
        {
            ViewBag.Message = "Service Detail";

            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "AboutUs";

            return View();
        }
        public ActionResult Team()
        {
            ViewBag.Message = "Our Team";

            return View();
        }
        public ActionResult PropertyList()
        {
            ViewBag.Message = "Property List";

            return View();
        }

        public ActionResult RentalList()
        {
            ViewBag.Message = "Rental Property List";
            return View();
        }

        public ActionResult ForSaleList()
        {
            ViewBag.Message = "For Sale Property List";
            return View();
        }
    }
}
