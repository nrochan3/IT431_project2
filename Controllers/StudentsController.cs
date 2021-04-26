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
    public class StudentsController : Controller
    {
        private StudentContext db = new StudentContext();

        // GET: Students
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string search)
        {
            var students = db.Students.Include(s => s.AdmissionDecision).Include(s => s.Gender).Include(s => s.Major).Include(s => s.Semester);

            if (!String.IsNullOrEmpty(search))
            {
                students = students.Where(s => s.SSN.Contains(search) ||
                s.LastName.Contains(search));
            }

            return View(students.ToList());
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            var admissionDecisionslist = db.AdmissionDecisions.ToList();
            var genderslist = db.Genders.ToList();
            var majorslist = db.Majors.ToList();
            var enrollmentSemesterslist = db.Semesters.ToList();

            ViewBag.AdmissionDecisionId = new SelectList(admissionDecisionslist, "DecisionId", "DecisionType");
            ViewBag.GenderId = new SelectList(genderslist, "GenderId", "GenderType");
            ViewBag.MajorId = new SelectList(majorslist, "MajorId", "MajorName");
            ViewBag.EnrollmentSemesterId = new SelectList(enrollmentSemesterslist, "SemesterId", "SemesterName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "StudentId,SSN,Name,MiddleName,LastName,Email,HomePhone,CellPhone,StreetAddress,City,State,ZipCode,DOB,GenderId,HSName,HSCity,GraduationDate,CurrentGPA,MathSATScore,VerbalSATScore,CombinedSATScore,MajorId,EnrollmentSemesterId,EnrollmentYear,SubmissionDate,AdmissionDecisionId")] Student student)
        {
            if (ModelState.IsValid)
            {
                var admissionDecisionslist = db.AdmissionDecisions.ToList();
                ViewBag.AdmissionDecisionId = new SelectList(admissionDecisionslist, "DecisionId", "DecisionType");

                var genderslist = db.Genders.ToList();
                ViewBag.GenderId = new SelectList(genderslist, "GenderId", "GenderType", student.GenderId);

                var majorslist = db.Majors.ToList();
                ViewBag.MajorId = new SelectList(majorslist, "MajorId", "MajorName");

                var enrollmentSemesterslist = db.Semesters.ToList();
                ViewBag.EnrollmentSemesterId = new SelectList(enrollmentSemesterslist, "SemesterId", "SemesterName");

                //Verify the SSN isn't already in the database
                Student matchingStudent = db.Students.Where(cm => string.Compare(cm.SSN, student.SSN, true) == 0).FirstOrDefault();

                if (student == null)
                {
                    return HttpNotFound();
                }

                //If found a match
                if (matchingStudent != null)
                {
                    ModelState.AddModelError("SSN", "Social Security Number must be unique.");
                    return View(student);
                }

                if (student.CurrentGPA < 3)
                {
                    ModelState.AddModelError("CurrentGPA", "Your current GPA does not meet the minimum qualifications!");
                    return View(student);
                }

                if (student.CombinedSATScore < 1000)
                {
                    ModelState.AddModelError("CombinedSATScore", "Your combined SAT Scores do not meet the minimum qualifications!");
                    return View(student);
                }

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            //ViewBag.AdmissionDecisionId = new SelectList(db.AdmissionDecisions, "DecisionId", "DecisionType", student.AdmissionDecisionId);
            //ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderType", student.GenderId);
            //ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName", student.MajorId);
            //ViewBag.EnrollmentSemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", student.EnrollmentSemesterId);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdmissionDecisionId = new SelectList(db.AdmissionDecisions, "DecisionId", "DecisionType", student.AdmissionDecisionId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderType", student.GenderId);
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName", student.MajorId);
            ViewBag.EnrollmentSemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", student.EnrollmentSemesterId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StudentId,SSN,Name,MiddleName,LastName,Email,HomePhone,CellPhone,StreetAddress,City,State,ZipCode,DOB,GenderId,HSName,HSCity,GraduationDate,CurrentGPA,MathSATScore,VerbalSATScore,CombinedSATScore,MajorId,EnrollmentSemesterId,EnrollmentYear,SubmissionDate,AdmissionDecisionId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdmissionDecisionId = new SelectList(db.AdmissionDecisions, "DecisionId", "DecisionType", student.AdmissionDecisionId);
            ViewBag.GenderId = new SelectList(db.Genders, "GenderId", "GenderType", student.GenderId);
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName", student.MajorId);
            ViewBag.EnrollmentSemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", student.EnrollmentSemesterId);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
