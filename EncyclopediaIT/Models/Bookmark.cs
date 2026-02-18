using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopediaIT.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TechnologyId { get; set; }
        public Technology Technology { get; set; }
    }

}
