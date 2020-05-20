using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_App_Crud.Models;

namespace Mvc_App_Crud.Controllers
{
    public class AssuntoesController : Controller
    {
        private EscolaEntities db = new EscolaEntities();

        // GET: Assuntoes
        public ActionResult Index()
        {
            return View(db.Assunto.ToList());
        }

        // GET: Assuntoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assunto assunto = db.Assunto.Find(id);
            if (assunto == null)
            {
                return HttpNotFound();
            }
            return View(assunto);
        }

        // GET: Assuntoes/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssuntoID,Assunto1")] Assunto assunto)
        {
            if (ModelState.IsValid)
            {
                db.Assunto.Add(assunto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assunto);
        }

        // GET: Assuntoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assunto assunto = db.Assunto.Find(id);
            if (assunto == null)
            {
                return HttpNotFound();
            }
            return View(assunto);
        }

        // POST: Assuntoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssuntoID,Assunto1")] Assunto assunto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assunto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assunto);
        }

        // GET: Assuntoes/Delete/5
        public ActionResult Delete(int? id)
        {
            Assunto assunto = db.Assunto.Find(id);
            Aluno al = db.Aluno.Find(assunto.AssuntoID);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (al == null)
            {
                if (assunto == null)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                throw new Exception("Existe aluno associado ao assunto.");
            }
            return View(assunto);
        }

        // POST: Assuntoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assunto assunto = db.Assunto.Find(id);
            db.Assunto.Remove(assunto);
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
