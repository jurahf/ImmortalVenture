using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmortalVentureView.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MedicalView()
        {
            return View();
        }

        public ActionResult SuperviserDoctor()
        {
            return View();
        }

        public ActionResult SuperviserDriver()
        {
            return View();
        }

        public ActionResult Driver()
        {
            return View();
        }

        public ActionResult Testing()
        {
            return View();
        }
    }
}