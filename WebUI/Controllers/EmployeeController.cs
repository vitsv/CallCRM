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
using Domain.Concrete;

namespace callCRM.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EmployeeController : Controller
    {
        private IRepository<Employee> EmployeeRepository;

        public EmployeeController(IRepository<Employee> Repository)
        {
            EmployeeRepository = Repository;
        }

        //
        // GET: /Employee/

        public ViewResult Index()
        {
            return View(EmployeeRepository.GetAll().ToList());
        }

        //
        // GET: /Employee/Details/5

        public ViewResult Details(decimal id)
        {
            Employee employee = EmployeeRepository.GetById((int)id);
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeRepository.Insert(employee);
                employee.CreationDate = DateTime.Now;
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(decimal id)
        {
            Employee employee = EmployeeRepository.GetById((int)id);
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeRepository.Update(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(decimal id)
        {
            Employee employee = EmployeeRepository.GetById((int)id);
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Employee employee = EmployeeRepository.GetById((int)id);
            EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }

        public ViewResult Statistics(DateTime? start, DateTime? end)
        {
            var repository = new EFEmployeeRepository();
            DateTime startTime;
            DateTime endTime;
            if (start.HasValue)
                startTime = start.Value;
            else
                startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (end.HasValue)
                endTime = end.Value;
            else
                endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            ViewBag.start = startTime;
            ViewBag.end = endTime;

            return View(repository.GetStatistics(startTime,endTime));
        }
    }
}