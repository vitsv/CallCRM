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
    public class IVRDefController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /IVRDef/

        public ViewResult Index()
        {
            return View(db.AsteriskIVRDefinitions.ToList());
        }

        //
        // GET: /IVRDef/Details/5

        public ViewResult Details(int id)
        {
            AsteriskIVRDefinition asteriskivrdefinition = db.AsteriskIVRDefinitions.Find(id);
            var asteriskivrsteps = db.AsteriskIVRSteps.Where(s => s.AsteriskIVRDefinitionID == id);
            ViewBag.Steps = asteriskivrsteps.ToList();
            return View(asteriskivrdefinition);
        }

        //
        // GET: /IVRDef/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /IVRDef/Create

        [HttpPost]
        public ActionResult Create(AsteriskIVRDefinition asteriskivrdefinition)
        {
            if (ModelState.IsValid)
            {
                asteriskivrdefinition.CreationDate = DateTime.Now;
                db.AsteriskIVRDefinitions.Add(asteriskivrdefinition);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(asteriskivrdefinition);
        }
        
        //
        // GET: /IVRDef/Edit/5
 
        public ActionResult Edit(int id)
        {
            AsteriskIVRDefinition asteriskivrdefinition = db.AsteriskIVRDefinitions.Find(id);
            return View(asteriskivrdefinition);
        }

        //
        // POST: /IVRDef/Edit/5

        [HttpPost]
        public ActionResult Edit(AsteriskIVRDefinition asteriskivrdefinition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asteriskivrdefinition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asteriskivrdefinition);
        }

        //
        // GET: /IVRDef/Delete/5
 
        public ActionResult Delete(int id)
        {
            AsteriskIVRDefinition asteriskivrdefinition = db.AsteriskIVRDefinitions.Find(id);
            return View(asteriskivrdefinition);
        }

        //
        // POST: /IVRDef/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            AsteriskIVRDefinition asteriskivrdefinition = db.AsteriskIVRDefinitions.Find(id);
            db.AsteriskIVRDefinitions.Remove(asteriskivrdefinition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}