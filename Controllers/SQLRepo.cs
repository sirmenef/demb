using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demb.Models;
using Microsoft.EntityFrameworkCore;

namespace Demb.Controllers
{
    public class SQLRepo 
    {
        private readonly AppDbContext _context;

        public SQLRepo(AppDbContext context)
        {
            _context = context;
        }
        public Contacts GetContact(int id)
        {
            var cont = _context.contacts.FirstOrDefault(x => x.Id == id);
            return cont;
        }

        public Contacts EditContact(Contacts contacts)
        {
            var model = _context.contacts.Attach(contacts);
            model.State = EntityState.Modified;
            _context.SaveChanges();
            return contacts;

        }

        public Contacts DeleteContact(int id)
        {
            var contact = _context.contacts.FirstOrDefault(x => x.Id == id);
            _context.contacts.Remove(contact);
            _context.SaveChanges();
            return contact;
        }

        public Contacts AddContact(Contacts contacts)
        {
            var a = contacts;
            _context.contacts.Add(a);
            _context.SaveChanges();
            return contacts;
        }

        public IEnumerable<Contacts> ListContacts()
        {
            return _context.contacts;
        }
    }
    
}
