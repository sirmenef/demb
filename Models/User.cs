using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demb.Models
{
    public class User
    {
        [Column("id")]
        public int id { get; set; }
        [Required]
        [Column("email")]
        public string Email { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }



    }
}
