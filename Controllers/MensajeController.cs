using Parcial3Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial3Movil.Controllers
{
    public class MensajeController : Controller
    {
        private dbParcialEntities db = new dbParcialEntities();
        // GET: Mensaje
        public ActionResult Chat()
        {
            var cedula = "";
            if (System.Web.HttpContext.Current.Session["numCedula"] != null)
            {
                cedula = System.Web.HttpContext.Current.Session["numCedula"].ToString();
            }

            var cedulaTel = long.Parse(cedula);

            var Telefono = (from telefono in db.Telefono
                            where telefono.CedulaPersona == cedulaTel
                            select telefono).ToList<Telefono>();

            var Contactos = (from telefono in db.Telefono
                            where telefono.CedulaPersona != cedulaTel
                            select telefono).ToList<Telefono>();

            ViewBag.misTelefonos = new SelectList(Telefono, "NroTelefono", "NroTelefono");
            ViewBag.misContactos = new SelectList(Contactos, "NroTelefono", "NroTelefono");

            return View();
        }

        public ActionResult IniciarChat(long Telefono1, long Telefono2)
        {
            var contChat = (from d in db.Mensaje
                            where (d.NroTelefonoOrigen == Telefono1 && d.NroTelefonoDestino == Telefono2) || (d.NroTelefonoDestino == Telefono1 && d.NroTelefonoOrigen == Telefono2)
                            select d).FirstOrDefault();

            if (contChat == null)
            {
                Mensaje msj = new Mensaje();
                msj.NroTelefonoOrigen = Telefono1;
                msj.NroTelefonoDestino = Telefono2;
                db.Mensaje.Add(msj);
                db.SaveChanges();
                Session["numRemitente"] = Telefono1;
                Session["numDestinatario"] = Telefono2;

                var id = db.Mensaje.Max(e => e.MensajeId);
                Session["Mensajes"] = id;
                return RedirectToAction("Index", "MensajeChat");
            }
            else
            {
                Session["numRemitente"] = Telefono1;
                Session["numDestinatario"] = Telefono2;
                Session["Mensajes"] = contChat.MensajeId;
                return RedirectToAction("Index", "MensajeChat");
            }

            return RedirectToAction("Chat");
        }
    }
}