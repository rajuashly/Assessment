using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Models
{
    public class SignInResult
    {
        public string email { get; set; }

        public string accesstoken { get; set; }

        public string refreshtoken { get; set; }
    }
}
