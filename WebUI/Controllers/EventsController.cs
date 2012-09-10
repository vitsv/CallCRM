using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using callCRM.Models;
using Domain.Abstract;
using Domain.Entities;

namespace callCRM.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private IEventRepository EventRepository;

        public EventsController(IEventRepository Repository)
        {
            EventRepository = Repository;
        }

        //
        // GET: /Events/

        public ViewResult Index(string employeeID)
        {

            var currentUser = Membership.GetUser(User.Identity.Name);
            var employee = EventRepository.GetEmployee((Guid)currentUser.ProviderUserKey);

            if (employee == null)
                throw new Exception("No user found!");

            IQueryable<Event> events = null;

            var emplList = EventRepository.GetEmployees().Select(e => new
            {
                Key = e.EmployeeID,
                Value = e.LastName
            }).ToList();

            emplList.Add(new { Key = -1, Value = "All" });
            emplList.Add(new { Key = -2, Value = "Unassigned" });

            if (String.IsNullOrEmpty(employeeID))
            {
                events = EventRepository.SearchFor(e => e.EmployeeID == employee.EmployeeID);
                ViewBag.employeeID = new SelectList(emplList, "Key", "Value", employee.EmployeeID);
            }
            else
            {
                int _employeeID;

                if (!int.TryParse(employeeID, out _employeeID))
                    throw new Exception("Incorrect employee ID!");
                switch (_employeeID)
                {
                    case -1:
                        events = EventRepository.GetAll(); break;
                    case -2:
                        events = EventRepository.SearchFor(e => e.EmployeeID == null); break;
                    default: events = EventRepository.SearchFor(e => e.EmployeeID == _employeeID); break;
                }
                ViewBag.employeeID = new SelectList(emplList, "Key", "Value", int.Parse(employeeID));
            }

            return View(events.ToList());
        }

        //
        // GET: /Events/Details/5

        public ViewResult Details(decimal id)
        {
            Event _event = EventRepository.GetById((int)id);
            ViewBag.EventCategory = EventRepository.GetEventCategory(_event.EventCategoryID);
            return View(_event);
        }

        public ActionResult PreCreate()
        {
            ViewBag.EventCategoryID = EventRepository.GetEventCategories();
            return View();
        }

        //
        // GET: /Events/Create

        public ActionResult Create(string category)
        {
            ViewBag.EmployeeID = new SelectList(EventRepository.GetEmployees(), "EmployeeID", "LastName");
            ViewBag.ClientID = new SelectList(EventRepository.GetClients(), "ClientID", "LastName");
            int categoryID = 0;
            if (!string.IsNullOrEmpty(category))
                categoryID = Convert.ToInt32(category);
            ViewBag.EventStatusID = new SelectList(EventRepository.GetEventStatuses().Where(e => e.EventCategoryID == categoryID), "EventStatusID", "Name");
            ViewBag.EventCategoryID = categoryID;
            ViewBag.EventCategory = EventRepository.GetEventCategory(categoryID);
            return View();
        }

        //
        // POST: /Events/Create

        [HttpPost]
        public ActionResult Create(Event _event)
        {
            if (ModelState.IsValid)
            {
                _event.CreationDate = DateTime.Now;
                _event.CreatedBy = User.Identity.Name;
                EventRepository.Insert(_event);
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(EventRepository.GetEmployees(), "EmployeeID", "LastName", _event.EmployeeID);
            ViewBag.ClientID = new SelectList(EventRepository.GetClients(), "ClientID", "LastName", _event.ClientID);
            ViewBag.EventStatusID = new SelectList(EventRepository.GetEventStatuses().ToList(), "EventStatusID", "Name", _event.EventStatusID);
            ViewBag.EventCategoryID = new SelectList(EventRepository.GetEventCategories(), "EventCategoryID", "Name", _event.EventCategoryID);

            return View(_event);
        }

        //
        // GET: /Events/Edit/5

        public ActionResult Edit(decimal id)
        {
            Event _event = EventRepository.GetById((int)id);

            ViewBag.EmployeeID = new SelectList(EventRepository.GetEmployees(), "EmployeeID", "LastName", _event.EmployeeID);
            ViewBag.ClientID = new SelectList(EventRepository.GetClients(), "ClientID", "LastName", _event.ClientID);

            ViewBag.EventStatusID = new SelectList(EventRepository.GetEventStatuses().Where(e => e.EventCategoryID == _event.EventCategoryID), "EventStatusID", "Name");
            ViewBag.EventCategoryID = _event.EventCategoryID;
            ViewBag.EventCategory = EventRepository.GetEventCategory(_event.EventCategoryID);
            return View(_event);
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        public ActionResult Edit(Event _event)
        {
            if (ModelState.IsValid)
            {
                EventRepository.Update(_event);
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(EventRepository.GetEmployees(), "EmployeeID", "LastName", _event.EmployeeID);
            ViewBag.ClientID = new SelectList(EventRepository.GetClients(), "ClientID", "LastName", _event.ClientID);
            ViewBag.EventStatusID = new SelectList(EventRepository.GetEventStatuses().ToList(), "EventStatusID", "Name", _event.EventStatusID);
            ViewBag.EventCategoryID = new SelectList(EventRepository.GetEventCategories(), "EventCategoryID", "Name", _event.EventCategoryID);
            return View(_event);
        }

        //
        // GET: /Events/Delete/5

        public ActionResult Delete(decimal id)
        {
            Event _event = EventRepository.GetById((int)id);
            return View(_event);
        }

        //
        // POST: /Events/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Event _event = EventRepository.GetById((int)id);
            EventRepository.Delete(_event);
            return RedirectToAction("Index");
        }
    }
}