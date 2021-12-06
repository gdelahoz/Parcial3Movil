using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial3Movil.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOut
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Salir()
        {
            Session["numCedula"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}