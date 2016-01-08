using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationProjetMVC.Models;

namespace WebApplicationProjetMVC.Controllers
{
    public class TravSoumisController : Controller
    {
        private DBIG3B8Entities db = new DBIG3B8Entities();

        // GET: TravSoumis
        public ActionResult Index(decimal idR, decimal idE)
        {
            var risqueModel = db.Risques.Find(idR);
            var emploiModel = db.Emplois.Find(idE);
            var tsModel = new TravSoumisViewModels
            {
                EmploiModel = emploiModel,
                RisqueModel = risqueModel
            };
            return View(tsModel);
        }

        // GET: TravSoumis/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravSoumi travSoumi = db.TravSoumis.Find(id);
            if (travSoumi == null)
            {
                return HttpNotFound();
            }
            return View(travSoumi);
        }

        // GET: TravSoumis/Create
        public ActionResult Create(decimal idR, decimal idE)
        {
            Creates(idR, idE);
            var x = db.Emplois.Find(idE);
            return View(x);
        }

        // POST: TravSoumis/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Creates(decimal idR, decimal idE)
        {
            TravSoumi t = new TravSoumi();
            t.idEmploi = idE;
            t.idRisque = idR;
            // AUTO INCREMENT
            db.TravSoumis.Add(t);
            db.SaveChanges();
            return RedirectToAction("Create");
        }

        // GET: TravSoumis/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravSoumi travSoumi = db.TravSoumis.Find(id);
            if (travSoumi == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmploi = new SelectList(db.Emplois, "idEmploi", "soumis", travSoumi.idEmploi);
            ViewBag.idRisque = new SelectList(db.Risques, "idRisque", "libelle", travSoumi.idRisque);
            return View(travSoumi);
        }

        // POST: TravSoumis/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTravSoum,idEmploi,idRisque")] TravSoumi travSoumi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travSoumi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmploi = new SelectList(db.Emplois, "idEmploi", "soumis", travSoumi.idEmploi);
            ViewBag.idRisque = new SelectList(db.Risques, "idRisque", "libelle", travSoumi.idRisque);
            return View(travSoumi);
        }

        // GET: TravSoumis/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravSoumi travSoumi = db.TravSoumis.Find(id);
            if (travSoumi == null)
            {
                return HttpNotFound();
            }
            return View(travSoumi);
        }

        // POST: TravSoumis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TravSoumi travSoumi = db.TravSoumis.Find(id);
            db.TravSoumis.Remove(travSoumi);
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
