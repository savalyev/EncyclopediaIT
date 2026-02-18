using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopediaIT.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
        public bool IsApproved { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TechnologyId { get; set; }
        public Technology Technology { get; set; }
    }
}
