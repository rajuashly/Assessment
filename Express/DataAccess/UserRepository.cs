using Express.Interfaces;
using Express.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Express.DataAccess
{
    public class UserRepository: IUserRepository
    {
        private readonly ExpressDB_APIContext _context;

        public UserRepository(ExpressDB_APIContext context)
        {
            _context = context;
        }
        BusinessLayer.Cryptography _cryptography = new BusinessLayer.Cryptography();

        public User GetUserByEmail(string username)
        {
            try
            {
                username = _cryptography.Decrypt(username);
                return _context.Users.Where(x => x.Username == username).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UserSignIn(Authenticate user)
        {
            bool isauthentic = false;
            if (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                string decryptedEmail = string.Empty;
                try
                {
                    decryptedEmail = _cryptography.Decrypt(user.Username);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                if (!string.IsNullOrWhiteSpace(decryptedEmail))
                {
                    var item = GetUser(decryptedEmail);
                    if (item != null && item.Id > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(item.PasswordHash) && !string.IsNullOrWhiteSpace(user.PasswordHash))
                        {
                            try
                            {
                                string dbPwd = _cryptography.Decrypt(item.PasswordHash);
                                string enteredPwd = _cryptography.Decrypt(user.PasswordHash);
                                if (dbPwd == enteredPwd)
                                {
                                    isauthentic = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
            return isauthentic;
        }
        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }
        public bool CheckLogin(Authenticate user)
        {
            bool isauthentic = false;
            if (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                if (!string.IsNullOrWhiteSpace(user.Username))
                {
                    var item = GetUser(user.Username);
                    if (item != null && item.Id > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(item.PasswordHash) && !string.IsNullOrWhiteSpace(user.PasswordHash))
                        {
                            try
                            {
                                string dbPwd = _cryptography.Decrypt(item.PasswordHash);
                                string enteredPwd = user.PasswordHash;
                                if (dbPwd == enteredPwd)
                                {
                                    isauthentic = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
            return isauthentic;
        }
    }
}
