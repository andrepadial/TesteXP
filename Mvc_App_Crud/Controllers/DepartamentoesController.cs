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
    public class DepartamentoesController : Controller
    {
        private EscolaEntities db = new EscolaEntities();

        // GET: Departamentoes
        public ActionResult Index()
        {
            return View(db.Departamento.ToList());
        }

        

        // GET: Departamentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: Departamentoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartamentoID,DeparatamentoNome")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                Departamento depto = db.Departamento.Find(departamento.DepartamentoID);                

                if (depto == null)
                {
                    db.Departamento.Add(departamento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {                    
                    throw new Exception("Departamento com ID já existente.");
                }
            }            

            return View(departamento);
        }

        // GET: Departamentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: Departamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartamentoID,DeparatamentoNome")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        // GET: Departamentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamento.Find(id);
            Aluno al = db.Aluno.Find(departamento.DepartamentoID);

            if (al == null)
            {
                if (departamento == null)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                throw new Exception("Existe aluno associado ao departamento.");
            }

            return View(departamento);
        }

        // POST: Departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = db.Departamento.Find(id);
            db.Departamento.Remove(departamento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Pesquisa()
        {
            //return View();
            using (var db = new EscolaEntities())
            {
                var depto = db.Departamento.ToList();
                return View(depto);
        }
        }

        [HttpPost]
        public ActionResult Pesquisa(int id, string nome)
        {
            return View(db.Departamento.Where(x => x.DepartamentoID == id).Where(y => y.DeparatamentoNome.Contains(nome)));
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
