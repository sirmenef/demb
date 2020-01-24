using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Demb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demb.Controllers
{
    [Route("api/contacts")]
    public class RemoteController : Controller
    {
        private readonly AppDbContext _context;
        private string mess_201 = "Status code: 201\n";
        private string mess_404 = "Status code: 404\n";


        public RemoteController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("")]
        public async Task<List<Contact>> Get()
        {
            try
            {
                return await _context.contacts.ToListAsync();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Contact> Get(int id)
        {
            try
            {
                return await _context.contacts.FirstOrDefaultAsync(Id => Id.Id == id);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpPost("")]
        public async Task<string> POST([FromBody]Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = _context.contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();
                    var mod = await _context.contacts.FirstOrDefaultAsync(x => x.Name == contact.Name);
                    string message = mess_201 + $"\n{contact.Name} with {mod.Id} was successfully created";
                    return message;
                }

                return "Status Code: Error 404";
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpPut("{id}")]
        public async Task<string> PUT(Contact contact, int id)
        {
            try
            {
                contact.Id = id;
                var model = _context.contacts.Attach(contact);
                model.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "Status Code: 200\nContacts successfully edited";
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> DELETE(int id)
        {
            try
            {
                var model = _context.contacts.FirstOrDefault(x => x.Id == id);
                _context.contacts.Remove(model);
                await _context.SaveChangesAsync();
                return "Status: 200.\nContacts removed successfully";
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpPost("bulk")]
        public async Task<string> POST([FromBody]List<Contact> contacts)
        {
            try
            {
                foreach (var con in contacts)
                {
                    if (ModelState.IsValid)
                    {
                        _context.contacts.Add(con);
                        await _context.SaveChangesAsync();
                    }
                }
                return "Status: 200";
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return null;
            }
        }

    }
}