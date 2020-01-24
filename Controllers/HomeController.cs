using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var model = _context.contacts;
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
        public IActionResult Create(Contact contct)
        {
            _context.contacts.Add(contct);
            _context.SaveChanges();
            return RedirectToAction("GetContact", contct);
        }


        [Route("/EditContact/{Id}")]
        [HttpPost]
        public IActionResult EditContact(Contact contact)
        {
            var model = _context.contacts.Attach(contact);
            model.State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("GetContact", new {id = contact.Id});
        }


        [Route("/EditContact/{Id}")]        
        [HttpGet]
        public IActionResult EditContact(int id)
        {
            var model = _context.contacts.FirstOrDefault(Id => Id.Id == id);
            return View("Edit", model);
        }

        [Route("/delete/{Id}")]
        public IActionResult DeleteContact(int id)
        {
            var  model = _context.contacts.FirstOrDefault(Id => Id.Id == id);
            _context.contacts.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("/details/{Id}")]
        public ActionResult GetContact(int id)
        {
            var model = _context.contacts.FirstOrDefault(Id => Id.Id == id);
            return View("Details", model);
        }
    }
}
