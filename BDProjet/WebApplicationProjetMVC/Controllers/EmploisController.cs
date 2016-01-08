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
    public class EmploisController : Controller
    {
        private DBIG3B8Entities db = new DBIG3B8Entities();

       /* // GET: Emplois
        public ActionResult Index()
        {
            var emplois = db.Emplois.Include(e => e.Entreprise).Include(e => e.Travailleur);
            return View(emplois.ToList());
        }*/

        // GET: Emplois/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emplois.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            return View(emploi);
        }

        // GET: Emplois/Create
        public ActionResult Create()
        {
            ViewBag.idEntreprise = new SelectList(db.Entreprises, "idEntreprise", "denomination");
            ViewBag.idTrav = new SelectList(db.Travailleurs, "idTrav", "nom");
            return View();
        }

        // POST: Emplois/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmploi,dateEmbauche,dateFin,soumis,idEntreprise,idTrav")] Emploi emploi)
        {
            if (ModelState.IsValid)
            {
                db.Emplois.Add(emploi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEntreprise = new SelectList(db.Entreprises, "idEntreprise", "denomination", emploi.idEntreprise);
            ViewBag.idTrav = new SelectList(db.Travailleurs, "idTrav", "nom", emploi.idTrav);
            return View(emploi);
        }

        // GET: Emplois/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emplois.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEntreprise = new SelectList(db.Entreprises, "idEntreprise", "denomination", emploi.idEntreprise);
            ViewBag.idTrav = new SelectList(db.Travailleurs, "idTrav", "nom", emploi.idTrav);
            return View(emploi);
        }

        // POST: Emplois/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmploi,dateEmbauche,dateFin,soumis,idEntreprise,idTrav")] Emploi emploi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emploi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEntreprise = new SelectList(db.Entreprises, "idEntreprise", "denomination", emploi.idEntreprise);
            ViewBag.idTrav = new SelectList(db.Travailleurs, "idTrav", "nom", emploi.idTrav);
            return View(emploi);
        }

        // GET: Emplois/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploi emploi = db.Emplois.Find(id);
            if (emploi == null)
            {
                return HttpNotFound();
            }
            return View(emploi);
        }

        // POST: Emplois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Emploi emploi = db.Emplois.Find(id);
            db.Emplois.Remove(emploi);
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

        
        public ActionResult Index(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Emploi> listEmplois = new List<Emploi>();
            var emplois = db.Emplois.ToList();
            foreach (Emploi emploi in emplois)
            {
                if (emploi.idEntreprise == id && emploi.dateFin == null)
                {
                    listEmplois.Add(emploi);
                }
            }
            if (listEmplois.Count() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(listEmplois);
            }
        }
    }
}
