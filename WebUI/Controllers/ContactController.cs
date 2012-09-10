using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;

namespace callCRM.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private IClientRepository ClientRepository;

        public ContactController(IClientRepository ClRepository)
        {
            ClientRepository = ClRepository;
        }

        public int GetIDFieldType(string name)
        {
            int ID;
            switch (name)
            {
                case "Email": ID = 1; break;
                case "Address": ID = 2; break;
                case "Phone": ID = 3; break;
                default: ID = 2; break;
            }
            return ID;
        }

        //
        // GET: /Contact/

        public ViewResult Index(string stage)
        {
            var person = ClientRepository.GetAll();

            ViewBag.ContactStages = ClientRepository.GetContactStages();

            int stageID;
            if (!String.IsNullOrEmpty(stage) && stage != "-1")
            {
                ViewBag.Stage = stage;
                stageID = Convert.ToInt32(stage);
                return View(person.Where(p => p.ContactStage.ContactStageID == stageID).ToList());
            }
            else
            {
                ViewBag.Stage = "-1";
                return View(person.ToList());
            }
        }


        //GET: /Contact/Details/5

        public ViewResult Details(decimal id)
        {
            Client person = ClientRepository.GetById((int)id);
            var contact_filed = ClientRepository.GetContactFieldType();
            var contact_filed_values = ClientRepository.GetContactFields().Where(c => c.RelateToID == person.ClientID).ToList();

            ViewData["CustomFields"] = contact_filed;
            ViewData["CustomFieldsValues"] = contact_filed_values;
            return View(person);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(ClientRepository.GetCompanies(), "CompanyID", "Name");
            ViewBag.ContactStageID = new SelectList(ClientRepository.GetContactStages(), "ContactStageID", "Name");
            ViewBag.EmployeeID = new SelectList(ClientRepository.GetEmployees(), "EmployeeID", "FirstName");

            ViewData["CustomFields"] = ClientRepository.GetContactFieldType();

            return View();
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(Client person)
        {
            var contactFields = ClientRepository.GetContactFieldType();
            ViewData["CustomFields"] = contactFields;
            if (ModelState.IsValid)
            {
                person.CreationDate = DateTime.Now;
                ClientRepository.Insert(person);

                //miara tymczasowa, zeby uzyskac id
                //PERSON personTmp = db.PERSON.Single(p => p.FIRST_NAME == person.FIRST_NAME && p.LAST_NAME == person.LAST_NAME);

                //TODO Przenies to do repository
                int i = 0;
                foreach (var cf in contactFields)
                {

                    var field = Request.Form[cf.Name + "[]"];
                    if (field != null)
                    {
                        string[] words = field.Split(',');
                        foreach (string word in words)
                        {

                            var contact_filed = new ContactField();
                            contact_filed.ContactFieldID = i;
                            contact_filed.ContactFieldTypeID = GetIDFieldType(cf.Name);
                            contact_filed.Value = word;
                            contact_filed.RelateToID = person.ClientID;
                            new EFContactFieldRepository().Insert(contact_filed);
                            i++;
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(ClientRepository.GetCompanies(), "CompanyID", "Name", person.CompanyID);
            ViewBag.ContactStageID = new SelectList(ClientRepository.GetContactStages(), "ContactStageID", "Name", person.ContactStageID);
            ViewBag.EmployeeID = new SelectList(ClientRepository.GetEmployees(), "EmployeeID", "FirstName", person.EmployeeID);
            return View(person);
        }

        //
        // GET: /Contact/Edit/5

        public ActionResult Edit(decimal id)
        {
            Client person = ClientRepository.GetById((int)id);
            ViewBag.CompanyID = new SelectList(ClientRepository.GetCompanies(), "CompanyID", "Name", person.CompanyID);
            ViewBag.ContactStageID = new SelectList(ClientRepository.GetContactStages(), "ContactStageID", "Name", person.ContactStageID);
            ViewBag.EmployeeID = new SelectList(ClientRepository.GetEmployees(), "EmployeeID", "FirstName", person.EmployeeID);
            return View(person);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(Client person)
        {
            if (ModelState.IsValid)
            {
                ClientRepository.Update(person);
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(ClientRepository.GetCompanies(), "CompanyID", "Name", person.CompanyID);
            ViewBag.ContactStageID = new SelectList(ClientRepository.GetContactStages(), "ContactStageID", "Name", person.ContactStageID);
            ViewBag.EmployeeID = new SelectList(ClientRepository.GetEmployees(), "EmployeeID", "FirstName", person.EmployeeID);
            return View(person);
        }

        //
        // GET: /Contact/Delete/5

        public ActionResult Delete(decimal id)
        {
            Client person = ClientRepository.GetById((int)id);
            return View(person);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Client person = ClientRepository.GetById((int)id);
            ClientRepository.Delete(person);
            return RedirectToAction("Index");
        }
    }
}