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
    public class AlunoesController : Controller
    {
        private EscolaEntities db = new EscolaEntities();

        // GET: Alunoes
        public ActionResult Index()
        {
            var aluno = db.Aluno.Include(a => a.Assunto).Include(a => a.Departamento);
            return View(aluno.ToList());
        }

        // GET: Alunoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Aluno.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // GET: Alunoes/Create
        public ActionResult Create()
        {
            ViewBag.AssuntoID = new SelectList(db.Assunto, "AssuntoID", "Assunto1");
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "DepartamentoID", "DeparatamentoNome");
            return View();
        }

        // POST: Alunoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlunoID,AlunoNome,DepartamentoID,AssuntoID")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Aluno.Add(aluno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssuntoID = new SelectList(db.Assunto, "AssuntoID", "Assunto1", aluno.AssuntoID);
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "DepartamentoID", "DeparatamentoNome", aluno.DepartamentoID);
            return View(aluno);
        }

        // GET: Alunoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Aluno.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssuntoID = new SelectList(db.Assunto, "AssuntoID", "Assunto1", aluno.AssuntoID);
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "DepartamentoID", "DeparatamentoNome", aluno.DepartamentoID);
            return View(aluno);
        }

        // POST: Alunoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlunoID,AlunoNome,DepartamentoID,AssuntoID")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssuntoID = new SelectList(db.Assunto, "AssuntoID", "Assunto1", aluno.AssuntoID);
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "DepartamentoID", "DeparatamentoNome", aluno.DepartamentoID);
            return View(aluno);
        }

        // GET: Alunoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Aluno.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: Alunoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluno aluno = db.Aluno.Find(id);
            db.Aluno.Remove(aluno);
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
