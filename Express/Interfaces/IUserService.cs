using Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string username);
        Task<bool> LogOut(string email);
        Task<SignInResult> SignIn(User user);
        //Task RefreshToken(string email);
    }
}
