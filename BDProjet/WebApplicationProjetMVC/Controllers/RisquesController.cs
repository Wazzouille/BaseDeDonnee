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
    public class RisquesController : Controller
    {
        private DBIG3B8Entities db = new DBIG3B8Entities();

        // GET: Risques
        public ActionResult Index(decimal id)
        {
            var risqueModel = db.Risques.ToList();
            List<Risque> listRisque = new List<Risque>();
            var emploiModel = db.Emplois.Find(id);
            var erModel = new EmploiRisqueViewModels();
            erModel.EmploiModel = emploiModel;
            var travSoumis = db.TravSoumis.ToList();
            if (travSoumis.Count() != 0)
            {
                foreach (Risque risque in risqueModel)
                {
                    var exist = false;
                    for (int i = 0; travSoumis.Count() > i; i++)
                    {
                        if (travSoumis[i].idEmploi == erModel.EmploiModel.idEmploi && travSoumis[i].idRisque == risque.idRisque)
                        {
                            exist = true;
                        }
                    }
                    if (!exist)
                    {
                        listRisque.Add(risque);
                    }
                }
                erModel.RisqueModel = listRisque;
            }
            else
            {
                erModel.RisqueModel = risqueModel;
            }
            return View(erModel);
        }

        // GET: Risques/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // GET: Risques/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Risques/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "libelle,idRisque")] Risque risque)
        {
            if (ModelState.IsValid)
            {
                db.Risques.Add(risque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(risque);
        }

        // GET: Risques/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // POST: Risques/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "libelle,idRisque")] Risque risque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(risque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(risque);
        }

        // GET: Risques/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Risque risque = db.Risques.Find(id);
            if (risque == null)
            {
                return HttpNotFound();
            }
            return View(risque);
        }

        // POST: Risques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Risque risque = db.Risques.Find(id);
            db.Risques.Remove(risque);
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
