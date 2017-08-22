using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CommentFormProject.Models;

namespace CommentFormProject.Controllers
{
    public class CommentFormsController : Controller
    {
        private CommentFormProjectContext db = new CommentFormProjectContext();

        // GET: CommentForms
        public ActionResult Index()
        {
            var commentForms = db.CommentForms.Include(c => c.Procedure);
            return View(commentForms.ToList());
        }

        // GET: CommentForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentForm commentForm = db.CommentForms.Find(id);
            if (commentForm == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CommentFormViewModel
            {
                CommentForm = commentForm,
                //We don't want to pull back all the procedures, just the one with the 
                //same priority as our comment
                Procedure = (from p in db.Procedures
                             where p.Priority == commentForm.Priority
                             select p).First()
            };
            return View(viewModel);




        }

        // GET: CommentForms/Create
        public ActionResult Create()
        {
            ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "Details");
            return View();
        }

        // POST: CommentForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentID,Name,Comment,Priority,ProcedureID")] CommentForm commentForm)
        {
            if (ModelState.IsValid)
            {
                db.CommentForms.Add(commentForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "Details", commentForm.ProcedureID);
            return View(commentForm);
        }

        // GET: CommentForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentForm commentForm = db.CommentForms.Find(id);
            if (commentForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "Details", commentForm.ProcedureID);
            return View(commentForm);
        }

        // POST: CommentForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,Name,Comment,Priority,ProcedureID")] CommentForm commentForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "Details", commentForm.ProcedureID);
            return View(commentForm);
        }

        // GET: CommentForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentForm commentForm = db.CommentForms.Find(id);
            if (commentForm == null)
            {
                return HttpNotFound();
            }
            return View(commentForm);
        }

        // POST: CommentForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentForm commentForm = db.CommentForms.Find(id);
            db.CommentForms.Remove(commentForm);
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
