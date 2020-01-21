using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demb.Models;
using Microsoft.EntityFrameworkCore;

namespace Demb.Controllers
{
    public class SQLRepo : IContactRepo
    {
        private readonly AppDbContext _context;

        public SQLRepo(AppDbContext context)
        {
            _context = context;
        }
        public contact GetContact(int id)
        {
            var cont = _context.contact.FirstOrDefault(x => x.id == id);
            return cont;
        }

        public contact EditContact(contact contact)
        {
            var model = _context.contact.Attach(contact);
            model.State = EntityState.Modified;
            _context.SaveChanges();
            return contact;

        }

        public contact DeleteContact(int id)
        {
            var contact = _context.contact.FirstOrDefault(x => x.id == id);
            _context.contact.Remove(contact);
            _context.SaveChanges();
            return contact;
        }

        public contact AddContact(contact contact)
        {
            var a = contact;
            _context.contact.Add(a);
            _context.SaveChanges();
            return contact;
        }

        public IEnumerable<contact> ListContacts()
        {
            return _context.contact;
        }
    }
    
}
