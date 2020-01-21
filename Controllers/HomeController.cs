using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demb.Controllers
{
    public class HomeController : Controller
    {
        private readonly SQLRepo _sql;

        public HomeController(SQLRepo sqlRepo)
        {
            _sql = sqlRepo;
        }

        [Route("/")]
        [Route("/home")]
        [Route("/index")]
        [Route("/home/index")]
        public IActionResult Index()
        {
            var model = _sql.ListContacts();
            return View(model);
        }

        [Route("/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("/Create")]
        public IActionResult Create(contact contct)
        {
            _sql.AddContact(contct);
            return RedirectToAction("GetContact", contct);
        }


        [Route("/EditContact/{id}")]
        [HttpPost]
        public IActionResult EditContact(contact contact)
        {
            _sql.EditContact(contact);
            return RedirectToAction("GetContact", new {id = contact.id});
        }


        [Route("/EditContact/{id}")]        
        [HttpGet]
        public IActionResult EditContact(int id)
        {
            var model = _sql.GetContact(id);
            return View("Edit", model);
        }

        [Route("/delete/{id}")]
        public IActionResult DeleteContact(int id)
        {
            _sql.DeleteContact(id);
            return RedirectToAction("Index");
        }

        [Route("/details/{id}")]
        public IActionResult GetContact(int id)
        {
            var model = _sql.GetContact(id);
            return View("Details", model);
        }
    }
}
