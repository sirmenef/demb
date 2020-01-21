using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demb.Models
{
    public interface IContactRepo
    {
        contact GetContact(int id);
        contact EditContact(contact contact);
        contact DeleteContact(int id);
        contact AddContact(contact contact);
        IEnumerable<contact> ListContacts();

    }
}
