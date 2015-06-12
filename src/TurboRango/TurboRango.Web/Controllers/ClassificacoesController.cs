using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurboRango.Dominio;
using TurboRango.Web.Models;

namespace TurboRango.Web.Controllers
{
    public class ClassificacoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Classificacoes
        public ActionResult Index(int? id)
        {
            return View(db.Classificacoes.ToList());
        }

        // GET: Classificacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classificacao classificacao = db.Classificacoes.Find(id);
            if (classificacao == null)
            {
                return HttpNotFound();
            }
            return View(classificacao);
        }

        // GET: Classificacao/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: Classificacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? id, [Bind(Include = "Nota, MediaNota")] Classificacao classificacao)
        {
            classificacao.DataAvaliacao = DateTime.Now;
            if (ModelState.IsValid)
            {
                
                db.Classificacoes.Add(classificacao);
                db.SaveChanges();
                return RedirectToAction("Index", "Classificacoes");
            }
            return View(classificacao);
        }

        // GET: Avaliacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classificacao classificacao = db.Classificacoes.Find(id);
            if (classificacao == null)
            {
                return HttpNotFound();
            }
            return View(classificacao);
        }

        // POST: Avaliacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nota, MediaNota, DataAvaliacao")] Classificacao classificacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classificacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Classificacoes");
            }
            return View(classificacao);
        }

        // GET: Avaliacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classificacao classificacao = db.Classificacoes.Find(id);
            if (classificacao == null)
            {
                return HttpNotFound();
            }
            return View(classificacao);
        }

        // POST: Avaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classificacao classificacao = db.Classificacoes.Find(id);
            db.Classificacoes.Remove(classificacao);
            db.SaveChanges();
            return RedirectToAction("Index", "Classificacoes");
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