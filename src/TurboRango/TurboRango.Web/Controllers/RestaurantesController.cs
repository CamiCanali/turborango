using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TurboRango.Dominio;
using TurboRango.Web.Models;

namespace TurboRango.Web.Controllers
{
    [Authorize]
    public class RestaurantesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Restaurantes
        public ActionResult Index()
        {
            var restaurantes = db.Restaurantes
                .Include(x => x.Contato)
                .Include(x => x.Localizacao);
            return View(restaurantes.ToList());
        }

        // GET: Restaurantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = PorId(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Capacidade,Nome,Categoria,Contato,Localizacao")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Restaurantes.Add(restaurante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurante);
        }

        // GET: Restaurantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = PorId(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Capacidade,Nome,Categoria,Contato,Localizacao")] Restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurante).State = EntityState.Modified;
                db.Entry(restaurante.Contato).State = EntityState.Modified;
                db.Entry(restaurante.Localizacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurante restaurante = PorId(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurante restaurante = PorId(id);
            db.Entry(restaurante.Contato).State = EntityState.Deleted;
            db.Entry(restaurante.Localizacao).State = EntityState.Deleted;
            db.Restaurantes.Remove(restaurante);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public JsonResult Restaurantes()
        {
            var todos = db.Restaurantes
                .Include(_ => _.Localizacao)
                .ToList();

            return Json(new
            {
                restaurantes = todos,
                camigoal = DateTime.Now
            }, JsonRequestBehavior.AllowGet);
        }

        private Restaurante PorId(int? id = 0)
        {
            return db.Restaurantes
                .Include(x => x.Localizacao)
                .Include(x => x.Contato)
                .FirstOrDefault(x => x.Id == id);
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
