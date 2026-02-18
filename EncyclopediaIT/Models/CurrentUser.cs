using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncyclopediaIT.Models
{
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static string Username { get; set; }
        public static bool IsAdmin { get; set; }
        public static bool IsAuthenticated { get; set; }

        public static DateTime RegistrationDate {  get; set; }
    }
}
