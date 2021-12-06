using Parcial3Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial3Movil.Controllers
{
    public class MensajeChatController : Controller
    {
        private dbParcialEntities db = new dbParcialEntities();
        // GET: MensajeChat
        public ActionResult Index()
        {
            var mensajeId = long.Parse(System.Web.HttpContext.Current.Session["Mensajes"].ToString());
            var convMensajes = (from d in db.DetalleMensaje
                                where (d.MensajeId == mensajeId)
                                select d).ToList<DetalleMensaje>();
            ViewBag.MensajesDetalles = convMensajes;
            ViewBag.destinatario = long.Parse(System.Web.HttpContext.Current.Session["numDestinatario"].ToString());
            return View();
        }

        public ActionResult MandarMensaje(string mensaje)
        {
            var mensajeId = long.Parse(System.Web.HttpContext.Current.Session["Mensajes"].ToString());
            var remitente = long.Parse(System.Web.HttpContext.Current.Session["numRemitente"].ToString());
            //Mensajes de conversacion
            

            DetalleMensaje msjDetalle = new DetalleMensaje();
            msjDetalle.MensajeId = mensajeId;
            msjDetalle.MensajeDescripcion = mensaje;
            msjDetalle.CostoId = 1;
            msjDetalle.Remitente = remitente;
            db.DetalleMensaje.Add(msjDetalle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}