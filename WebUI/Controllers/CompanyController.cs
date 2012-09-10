using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;

namespace callCRM.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private ICompanyRepository CompanyRepository;

        public CompanyController(ICompanyRepository Repository)
        {
            CompanyRepository = Repository;
        }

        //
        // GET: /Company/

        public ViewResult Index(string stage)
        {
            var company = CompanyRepository.GetAll();

            ViewBag.ContactStages = CompanyRepository.GetContactStages();


            int stageID;
            if (!String.IsNullOrEmpty(stage) && stage != "-1")
            {
                ViewBag.Stage = stage;
                stageID = Convert.ToInt32(stage);
                return View(company.Where(p => p.ContactStage.ContactStageID == stageID).ToList());
            }
            else
            {
                ViewBag.Stage = "-1";
                return View(company.ToList());
            }
        }

        //
        // GET: /Company/Details/5

        public ViewResult Details(decimal id)
        {
            Company company = CompanyRepository.GetById((int)id);
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            ViewBag.ContactStageID = new SelectList(CompanyRepository.GetContactStages(), "ContactStageID", "NAME");
            return View();
        }

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                company.CreationDate = DateTime.Now.Date;
                CompanyRepository.Insert(company);
                return RedirectToAction("Index");
            }

            ViewBag.ContactStageID = new SelectList(CompanyRepository.GetContactStages(), "ContactStageID", "NAME");
            return View(company);
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit(decimal id)
        {
            Company company = CompanyRepository.GetById((int)id);
            ViewBag.ContactStageID = new SelectList(CompanyRepository.GetContactStages(), "ContactStageID", "NAME");
            return View(company);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                CompanyRepository.Update(company);
                return RedirectToAction("Index");
            }
            ViewBag.ContactStageID = new SelectList(CompanyRepository.GetContactStages(), "ContactStageID", "NAME");
            return View(company);
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(decimal id)
        {
            Company company = CompanyRepository.GetById((int)id);
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Company company = CompanyRepository.GetById((int)id);
            CompanyRepository.Delete(company);
            return RedirectToAction("Index");
        }
    }
}