using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demb.Models
{
    public class Contacts
    {
        [Column("id")]
        public int Id { get; set; }


        [Column("name")]
        public string Name { get; set; }


        [Column("phone")]
        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }


        [Column("email")]
        public string Email { get; set; }

    }
}
