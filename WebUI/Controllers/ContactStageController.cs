using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using callCRM.Models;
using Domain.Abstract;
using Domain.Entities;

namespace callCRM.Controllers
{ 
    public class ContactStageController : Controller
    {
                private IRepository<ContactStage> ContactStageRepository;

        public ContactStageController(IRepository<ContactStage> Repository)
        {
            ContactStageRepository = Repository;
        }

        //
        // GET: /ContactStage/

        public ViewResult Index()
        {
            return View(ContactStageRepository.GetAll().ToList());
        }

        //
        // GET: /ContactStage/Details/5

        public ViewResult Details(decimal id)
        {
            ContactStage contact_stage = ContactStageRepository.GetById((int)id);
            return View(contact_stage);
        }

        //
        // GET: /ContactStage/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ContactStage/Create

        [HttpPost]
        public ActionResult Create(ContactStage contact_stage)
        {
            if (ModelState.IsValid)
            {
                ContactStageRepository.Insert(contact_stage);
                return RedirectToAction("Index");  
            }

            return View(contact_stage);
        }
        
        //
        // GET: /ContactStage/Edit/5
 
        public ActionResult Edit(decimal id)
        {
            ContactStage contact_stage = ContactStageRepository.GetById((int)id);
            return View(contact_stage);
        }

        //
        // POST: /ContactStage/Edit/5

        [HttpPost]
        public ActionResult Edit(ContactStage contact_stage)
        {
            if (ModelState.IsValid)
            {
                ContactStageRepository.Update(contact_stage);
                return RedirectToAction("Index");
            }
            return View(contact_stage);
        }

        //
        // GET: /ContactStage/Delete/5
 
        public ActionResult Delete(decimal id)
        {
            ContactStage contact_stage = ContactStageRepository.GetById((int)id);
            return View(contact_stage);
        }

        //
        // POST: /ContactStage/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ContactStage contact_stage = ContactStageRepository.GetById((int)id);
            ContactStageRepository.Delete(contact_stage);
            return RedirectToAction("Index");
        }
    }
}