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
    public class ExamenParticuliersController : Controller
    {
        private DBIG3B8Entities db = new DBIG3B8Entities();

        // GET: ExamenParticuliers
        public ActionResult Index(decimal idEx, decimal idEm)
        {
            var examenModel = db.TypeExamen.Find(idEx);
            var emploiModel = db.Emplois.Find(idEm);
            var epModel = new ExamPartiViewModel
            {
                EmploiModel = emploiModel,
                ExamenModel = examenModel
            };
            return View(epModel);
        }

        // GET: ExamenParticuliers/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamenParticulier examenParticulier = db.ExamenParticuliers.Find(id);
            if (examenParticulier == null)
            {
                return HttpNotFound();
            }
            return View(examenParticulier);
        }

        // GET: ExamenParticuliers/Create
        public ActionResult Create(decimal idEx, decimal idEm)
        {
            Creates(idEx, idEm);
            var x = db.Emplois.Find(idEm);
            return View(x);
        }

        // POST: ExamenParticuliers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Creates(decimal idEx, decimal idEm)
        {
            ExamenParticulier ep = new ExamenParticulier();
            ep.idEmploi = idEm;
            ep.idExamen = idEx;
            db.ExamenParticuliers.Add(ep);
            db.SaveChanges();
            return RedirectToAction("Create");
        }

        // GET: ExamenParticuliers/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamenParticulier examenParticulier = db.ExamenParticuliers.Find(id);
            if (examenParticulier == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmploi = new SelectList(db.Emplois, "idEmploi", "soumis", examenParticulier.idEmploi);
            ViewBag.idExamen = new SelectList(db.TypeExamen, "idExamen", "libelle", examenParticulier.idExamen);
            return View(examenParticulier);
        }

        // POST: ExamenParticuliers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idExamPart,idEmploi,idExamen")] ExamenParticulier examenParticulier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examenParticulier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmploi = new SelectList(db.Emplois, "idEmploi", "soumis", examenParticulier.idEmploi);
            ViewBag.idExamen = new SelectList(db.TypeExamen, "idExamen", "libelle", examenParticulier.idExamen);
            return View(examenParticulier);
        }

        // GET: ExamenParticuliers/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamenParticulier examenParticulier = db.ExamenParticuliers.Find(id);
            if (examenParticulier == null)
            {
                return HttpNotFound();
            }
            return View(examenParticulier);
        }

        // POST: ExamenParticuliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ExamenParticulier examenParticulier = db.ExamenParticuliers.Find(id);
            db.ExamenParticuliers.Remove(examenParticulier);
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
