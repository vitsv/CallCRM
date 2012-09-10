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
    public class EventStatusController : Controller
    {
        private IEventStatusRepository EventStatusRepository;

        public EventStatusController(IEventStatusRepository Repository)
        {
            EventStatusRepository = Repository;
        }

        //
        // GET: /EventStatus/

        public ViewResult Index()
        {
            return View(EventStatusRepository.GetAll().ToList());
        }

        //
        // GET: /EventStatus/Details/5

        public ViewResult Details(decimal id)
        {
            EventStatus event_status = EventStatusRepository.GetById((int)id);
            return View(event_status);
        }

        //
        // GET: /EventStatus/Create

        public ActionResult Create()
        {
            ViewBag.EventCategoryID = new SelectList(EventStatusRepository.GetEventCategories(), "EventCategoryID", "Name");
            return View();
        }

        //
        // POST: /EventStatus/Create

        [HttpPost]
        public ActionResult Create(EventStatus event_status)
        {
            if (ModelState.IsValid)
            {
                EventStatusRepository.Insert(event_status);
                return RedirectToAction("Index");
            }

            return View(event_status);
        }

        //
        // GET: /EventStatus/Edit/5

        public ActionResult Edit(decimal id)
        {
            EventStatus event_status = EventStatusRepository.GetById((int)id);
            ViewBag.EventCategoryID = new SelectList(EventStatusRepository.GetEventCategories(), "EventCategoryID", "Name", event_status.EventCategoryID);
            return View(event_status);
        }

        //
        // POST: /EventStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(EventStatus event_status)
        {
            if (ModelState.IsValid)
            {
                EventStatusRepository.Update(event_status);
                return RedirectToAction("Index");
            }
            return View(event_status);
        }

        //
        // GET: /EventStatus/Delete/5

        public ActionResult Delete(decimal id)
        {
            EventStatus event_status = EventStatusRepository.GetById((int)id);
            return View(event_status);
        }

        //
        // POST: /EventStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            EventStatus event_status = EventStatusRepository.GetById((int)id);
            EventStatusRepository.Delete(event_status);
            return RedirectToAction("Index");
        }
    }
}