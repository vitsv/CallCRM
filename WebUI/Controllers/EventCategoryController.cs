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
    public class EventCategoryController : Controller
    {
        private IRepository<EventCategory> EventCategoryRepository;

        public EventCategoryController(IRepository<EventCategory> Repository)
        {
            EventCategoryRepository = Repository;
        }

        //
        // GET: /EventCategory/

        public ViewResult Index()
        {
            return View(EventCategoryRepository.GetAll().ToList());
        }

        //
        // GET: /EventCategory/Details/5

        public ViewResult Details(decimal id)
        {
            EventCategory event_category = EventCategoryRepository.GetById((int)id);
            return View(event_category);
        }

        //
        // GET: /EventCategory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /EventCategory/Create

        [HttpPost]
        public ActionResult Create(EventCategory event_category)
        {
            if (ModelState.IsValid)
            {
                EventCategoryRepository.Insert(event_category);
                return RedirectToAction("Index");
            }

            return View(event_category);
        }

        //
        // GET: /EventCategory/Edit/5

        public ActionResult Edit(decimal id)
        {
            EventCategory event_category = EventCategoryRepository.GetById((int)id);
            return View(event_category);
        }

        //
        // POST: /EventCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(EventCategory event_category)
        {
            if (ModelState.IsValid)
            {
                EventCategoryRepository.Update(event_category);
                return RedirectToAction("Index");
            }
            return View(event_category);
        }

        //
        // GET: /EventCategory/Delete/5

        public ActionResult Delete(decimal id)
        {
            EventCategory event_category = EventCategoryRepository.GetById((int)id);
            return View(event_category);
        }

        //
        // POST: /EventCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            EventCategory event_category = EventCategoryRepository.GetById((int)id);
            EventCategoryRepository.Delete(event_category);
            return RedirectToAction("Index");
        }
    }
}