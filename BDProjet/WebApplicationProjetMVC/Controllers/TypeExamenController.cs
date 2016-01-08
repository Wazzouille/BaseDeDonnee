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
    public class TypeExamenController : Controller
    {
        private DBIG3B8Entities db = new DBIG3B8Entities();

        // GET: TypeExamen
        public ActionResult Index(decimal id)
        {
            var examenModel = db.TypeExamen.ToList();
            List<TypeExaman> listExamen = new List<TypeExaman>();
            var emploiModel = db.Emplois.Find(id);
            var eeModel = new EmploiExamenViewModel();
            eeModel.EmploiModel = emploiModel;
            var examParti = db.ExamenParticuliers.ToList();
            var travS = db.TravSoumis.ToList();
            if (examParti.Count() != 0 || travS.Count() != 0)
            {
                foreach (TypeExaman examen in examenModel)
                {
                    var exist = false;
                    for (int i = 0; examParti.Count() > i; i++)
                    {
                        if (examParti[i].idEmploi == eeModel.EmploiModel.idEmploi && examParti[i].idExamen == examen.idExamen)
                        {
                            exist = true;
                        }
                    }
                    if (!exist)
                    {
                        for (int i = 0; travS.Count() > i; i++)
                        {
                            var r = db.Risques.Find(travS[i].idRisque);
                            if (travS[i].idEmploi == eeModel.EmploiModel.idEmploi && r.idRisque == examen.idRisque)
                            {
                                exist = true;
                            }
                        }
                        if (!exist)
                        {
                            listExamen.Add(examen);
                        }
                    }
                }
                eeModel.ExamenModel = listExamen;
            }
            else
            {
                eeModel.ExamenModel = examenModel;
            }
            return View(eeModel);
        }

        // GET: TypeExamen/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeExaman typeExaman = db.TypeExamen.Find(id);
            if (typeExaman == null)
            {
                return HttpNotFound();
            }
            return View(typeExaman);
        }

        // GET: TypeExamen/Create
        public ActionResult Create()
        {
            ViewBag.idRisque = new SelectList(db.Risques, "idRisque", "libelle");
            return View();
        }

        // POST: TypeExamen/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idExamen,libelle,periodicite,prixTS,prixNS,CompteDeProduit,idRisque")] TypeExaman typeExaman)
        {
            if (ModelState.IsValid)
            {
                db.TypeExamen.Add(typeExaman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRisque = new SelectList(db.Risques, "idRisque", "libelle", typeExaman.idRisque);
            return View(typeExaman);
        }

        // GET: TypeExamen/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeExaman typeExaman = db.TypeExamen.Find(id);
            if (typeExaman == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRisque = new SelectList(db.Risques, "idRisque", "libelle", typeExaman.idRisque);
            return View(typeExaman);
        }

        // POST: TypeExamen/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idExamen,libelle,periodicite,prixTS,prixNS,CompteDeProduit,idRisque")] TypeExaman typeExaman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeExaman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRisque = new SelectList(db.Risques, "idRisque", "libelle", typeExaman.idRisque);
            return View(typeExaman);
        }

        // GET: TypeExamen/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeExaman typeExaman = db.TypeExamen.Find(id);
            if (typeExaman == null)
            {
                return HttpNotFound();
            }
            return View(typeExaman);
        }

        // POST: TypeExamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TypeExaman typeExaman = db.TypeExamen.Find(id);
            db.TypeExamen.Remove(typeExaman);
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
