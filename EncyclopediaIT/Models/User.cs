using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EncyclopediaIT.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
