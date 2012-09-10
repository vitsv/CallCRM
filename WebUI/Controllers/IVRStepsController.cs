using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Concrete;

namespace WebUI.Controllers
{ 
    public class IVRStepsController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /IVRSteps/

        public ViewResult Index()
        {
            var asteriskivrsteps = db.AsteriskIVRSteps.Include(a => a.AsteriskIVRDefinition);
            return View(asteriskivrsteps.ToList());
        }

        //
        // GET: /IVRSteps/Details/5

        public ViewResult Details(int id)
        {
            AsteriskIVRStep asteriskivrstep = db.AsteriskIVRSteps.Find(id);
            ViewBag.AsteriskIVRDefinitionID = new SelectList(db.AsteriskIVRDefinitions, "AsteriskIVRDefinitionID", "Name", asteriskivrstep.AsteriskIVRDefinitionID);
            return View(asteriskivrstep);
        }

        //
        // GET: /IVRSteps/Create

        public ActionResult Create(int id)
        {
            ViewBag.AsteriskIVRDefinitionID = new SelectList(db.AsteriskIVRDefinitions, "AsteriskIVRDefinitionID", "Name");
            ViewBag.IVRDefenitionID = id;
            return View();
        } 

        //
        // POST: /IVRSteps/Create

        [HttpPost]
        public ActionResult Create(AsteriskIVRStep asteriskivrstep)
        {
            if (ModelState.IsValid)
            {
                db.AsteriskIVRSteps.Add(asteriskivrstep);
                db.SaveChanges();
                return RedirectToAction("Details", "IVRDef", new { id = asteriskivrstep.AsteriskIVRDefinitionID });  
            }

            ViewBag.AsteriskIVRDefinitionID = new SelectList(db.AsteriskIVRDefinitions, "AsteriskIVRDefinitionID", "Name", asteriskivrstep.AsteriskIVRDefinitionID);
            return View(asteriskivrstep);
        }
        
        //
        // GET: /IVRSteps/Edit/5
 
        public ActionResult Edit(int id)
        {
            AsteriskIVRStep asteriskivrstep = db.AsteriskIVRSteps.Find(id);
            ViewBag.AsteriskIVRDefinitionID = new SelectList(db.AsteriskIVRDefinitions, "AsteriskIVRDefinitionID", "Name", asteriskivrstep.AsteriskIVRDefinitionID);
            return View(asteriskivrstep);
        }

        //
        // POST: /IVRSteps/Edit/5

        [HttpPost]
        public ActionResult Edit(AsteriskIVRStep asteriskivrstep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asteriskivrstep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "IVRDef", new { id = asteriskivrstep.AsteriskIVRDefinitionID }); 
            }
            ViewBag.AsteriskIVRDefinitionID = new SelectList(db.AsteriskIVRDefinitions, "AsteriskIVRDefinitionID", "Name", asteriskivrstep.AsteriskIVRDefinitionID);
            return View(asteriskivrstep);
        }

        //
        // GET: /IVRSteps/Delete/5
 
        public ActionResult Delete(int id)
        {
            AsteriskIVRStep asteriskivrstep = db.AsteriskIVRSteps.Find(id);
            ViewBag.AsteriskIVRDefinitionID = new SelectList(db.AsteriskIVRDefinitions, "AsteriskIVRDefinitionID", "Name", asteriskivrstep.AsteriskIVRDefinitionID);
            return View(asteriskivrstep);
        }

        //
        // POST: /IVRSteps/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AsteriskIVRStep asteriskivrstep = db.AsteriskIVRSteps.Find(id);
            db.AsteriskIVRSteps.Remove(asteriskivrstep);
            db.SaveChanges();
            return RedirectToAction("Details", "IVRDef", new { id = asteriskivrstep.AsteriskIVRDefinitionID }); 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}