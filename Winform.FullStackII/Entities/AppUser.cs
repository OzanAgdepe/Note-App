using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winform.FullStackII.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
