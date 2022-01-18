using Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string username);
        bool UserSignIn(Authenticate user);
        User GetUser(string username);
        bool CheckLogin(Authenticate user);
    }
}
