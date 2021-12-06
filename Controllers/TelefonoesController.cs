using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parcial3Movil.Models;

namespace Parcial3Movil.Controllers
{
    public class TelefonoesController : Controller
    {
        private dbParcialEntities db = new dbParcialEntities();

        // GET: Telefonoes
        public ActionResult Index()
        {
            var telefono = db.Telefono.Include(t => t.Personas);
            return View(telefono.ToList());
        }

        // GET: Telefonoes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // GET: Telefonoes/Create
        public ActionResult Create()
        {
            ViewBag.CedulaPersona = new SelectList(db.Personas, "Cedula", "Nombre");
            return View();
        }

        // POST: Telefonoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NroTelefono,CedulaPersona,Saldo,Estado,Desvio")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                db.Telefono.Add(telefono);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CedulaPersona = new SelectList(db.Personas, "Cedula", "Nombre", telefono.CedulaPersona);
            return View(telefono);
        }

        // GET: Telefonoes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            ViewBag.CedulaPersona = new SelectList(db.Personas, "Cedula", "Nombre", telefono.CedulaPersona);
            return View(telefono);
        }

        // POST: Telefonoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NroTelefono,CedulaPersona,Saldo,Estado,Desvio")] Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefono).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CedulaPersona = new SelectList(db.Personas, "Cedula", "Nombre", telefono.CedulaPersona);
            return View(telefono);
        }

        // GET: Telefonoes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefono telefono = db.Telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // POST: Telefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Telefono telefono = db.Telefono.Find(id);
            db.Telefono.Remove(telefono);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
