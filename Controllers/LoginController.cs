using Parcial3Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial3Movil.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        private dbParcialEntities db = new dbParcialEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.idRol = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Cedula,Nombre,Apellido,Direccion,Correo,Password,idRol")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(personas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRol = new SelectList(db.Usuarios, "Id", "Nombre", personas.idRol);
            return View(personas);
        }

        public ActionResult Ingresar(int cedula, string pass)
        {
            try
            {
                using (Models.dbParcialEntities db = new Models.dbParcialEntities())
                {
                    var User = (from persona in db.Personas
                                where persona.Cedula == cedula && persona.Password == pass.Trim()
                                select persona).FirstOrDefault();


                    if (User != null)
                    {
                        Session["tipoUsuario"] = User.Usuarios;
                        Session["numCedula"] = User.Cedula;
                        Session["usuario"] = User;
                        return RedirectToAction("Index", "Ingreso");
                    }

                    else
                    {
                        ViewBag.Error = "Cedula o Contraseña Invalido";
                        return RedirectToAction("Create", "Personas");

                    }
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}