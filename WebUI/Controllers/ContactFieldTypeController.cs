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
    public class ContactFieldTypeController : Controller
    {
        private IRepository<ContactFieldType> ContactFieldTypeRepository;

        public ContactFieldTypeController(IRepository<ContactFieldType> Repository)
        {
            ContactFieldTypeRepository = Repository;
        }

        //
        // GET: /CustomFieldType/

        public ViewResult Index()
        {
            return View(ContactFieldTypeRepository.GetAll().ToList());
        }

        //
        // GET: /CustomFieldType/Details/5

        public ViewResult Details(decimal id)
        {
            ContactFieldType contact_field_type = ContactFieldTypeRepository.GetById((int)id);
            return View(contact_field_type);
        }

        //
        // GET: /CustomFieldType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /CustomFieldType/Create

        [HttpPost]
        public ActionResult Create(ContactFieldType contact_field_type)
        {
            if (ModelState.IsValid)
            {
                ContactFieldTypeRepository.Insert(contact_field_type);
                return RedirectToAction("Index");  
            }

            return View(contact_field_type);
        }
        
        //
        // GET: /CustomFieldType/Edit/5
 
        public ActionResult Edit(decimal id)
        {
            ContactFieldType contact_field_type = ContactFieldTypeRepository.GetById((int)id);
            return View(contact_field_type);
        }

        //
        // POST: /CustomFieldType/Edit/5

        [HttpPost]
        public ActionResult Edit(ContactFieldType contact_field_type)
        {
            if (ModelState.IsValid)
            {
                ContactFieldTypeRepository.Update(contact_field_type);
                return RedirectToAction("Index");
            }
            return View(contact_field_type);
        }

        //
        // GET: /CustomFieldType/Delete/5
 
        public ActionResult Delete(decimal id)
        {
            ContactFieldType contact_field_type = ContactFieldTypeRepository.GetById((int)id);
            return View(contact_field_type);
        }

        //
        // POST: /CustomFieldType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ContactFieldType contact_field_type = ContactFieldTypeRepository.GetById((int)id);
            ContactFieldTypeRepository.Delete(contact_field_type);
            return RedirectToAction("Index");
        }
    }
}