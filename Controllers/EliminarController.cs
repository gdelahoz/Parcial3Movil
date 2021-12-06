using Parcial3Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial3Movil.Controllers
{
    public class EliminarController : Controller
    {
        // GET: Eliminar
        public ActionResult Index()
        {
            var sesion = System.Web.HttpContext.Current.Session["numCedula"].ToString();
            var cedula = long.Parse(sesion);
            using (Models.dbParcialEntities db = new Models.dbParcialEntities())
            {
                var deTel = (from telefono in db.Telefono
                           where telefono.CedulaPersona != cedula
                           select telefono).ToList<Telefono>();
                SelectList listaTelefono = new SelectList(deTel, "NroTelefono", "NroTelefono");

                ViewBag.Origen = listaTelefono;

            }
            return View();
        }

        public ActionResult Eliminar(long NroTelefono)
        {

            using (Models.dbParcialEntities db = new Models.dbParcialEntities())
            {

                var deTel = (from telefono in db.Telefono
                           where telefono.NroTelefono == NroTelefono
                           select telefono).FirstOrDefault();

                var per = (from telefono in db.Telefono
                           where telefono.CedulaPersona == deTel.CedulaPersona
                           select telefono).ToList<Telefono>();
                if (per.Count() > 1)
                {
                    Telefono removerTel = db.Telefono.Find(NroTelefono);
                    db.Telefono.Remove(removerTel);
                    db.SaveChanges();
                }
                else
                {
                    Telefono removerTel = db.Telefono.Find(NroTelefono);
                    db.Telefono.Remove(removerTel);
                    db.SaveChanges();

                    Personas removerPer = db.Personas.Find(deTel.CedulaPersona);
                    db.Personas.Remove(removerPer);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Telefonoes");
        }
    }
}