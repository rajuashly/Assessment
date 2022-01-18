using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Models
{
    public class Authenticate
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
