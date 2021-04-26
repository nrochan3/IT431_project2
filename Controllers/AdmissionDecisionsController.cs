using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdmissionsWebsite;

namespace AdmissionsWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdmissionDecisionsController : Controller
    {
        private StudentContext db = new StudentContext();

        // GET: AdmissionDecisions
        public ActionResult Index()
        {
            return View(db.AdmissionDecisions.ToList());
        }

        // GET: AdmissionDecisions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdmissionDecision admissionDecision = db.AdmissionDecisions.Find(id);
            if (admissionDecision == null)
            {
                return HttpNotFound();
            }
            return View(admissionDecision);
        }

        // GET: AdmissionDecisions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdmissionDecisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DecisionId,DecisionType")] AdmissionDecision admissionDecision)
        {
            if (ModelState.IsValid)
            {
                db.AdmissionDecisions.Add(admissionDecision);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admissionDecision);
        }

        // GET: AdmissionDecisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdmissionDecision admissionDecision = db.AdmissionDecisions.Find(id);
            if (admissionDecision == null)
            {
                return HttpNotFound();
            }
            return View(admissionDecision);
        }

        // POST: AdmissionDecisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DecisionId,DecisionType")] AdmissionDecision admissionDecision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admissionDecision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admissionDecision);
        }

        // GET: AdmissionDecisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdmissionDecision admissionDecision = db.AdmissionDecisions.Find(id);
            if (admissionDecision == null)
            {
                return HttpNotFound();
            }
            return View(admissionDecision);
        }

        // POST: AdmissionDecisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdmissionDecision admissionDecision = db.AdmissionDecisions.Find(id);
            db.AdmissionDecisions.Remove(admissionDecision);
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
