using Express.Helpers;
using Express.Interfaces;
using Express.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        BusinessLayer.Cryptography _cryptography = new BusinessLayer.Cryptography();

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {

            User user = new User();
            user.ReturnUrl = string.Empty;

            if (HttpContext.Request.Cookies.ContainsKey("uname") && HttpContext.Request.Cookies.ContainsKey("pword"))
            {
                string cookieUsername = Request.Cookies["uname"];
                string cookiePassword = Request.Cookies["pword"];
                try
                {
                    var username = _cryptography.Decrypt(cookieUsername);
                    var password = _cryptography.Decrypt(cookiePassword);

                    var exists = await _userService.GetUserByEmail(username);

                    if (exists == null || exists.Id == 0)
                    {
                        //ModelState.AddModelError("", "Invalid login attempt.");
                    }
                    else
                    {
                        user.Username = username;
                        user.PasswordHash = password;
                        var result = await _userService.SignIn(user);

                        if (!string.IsNullOrWhiteSpace(result.accesstoken) && !string.IsNullOrWhiteSpace(result.email))
                        {
                            HttpContext.Session.SetString("JWToken", result.accesstoken);
                            return RedirectToLocal(user.ReturnUrl);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            else
            {
                await RemoveCookiesandTokenAsync();
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                User exists = await _userService.GetUserByEmail(user.Username);

                if (exists == null || exists.Id == 0)
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
                else
                {
                    try
                    {
                        Models.SignInResult result = await _userService.SignIn(user);

                        if (!string.IsNullOrWhiteSpace(result.email) && !string.IsNullOrWhiteSpace(result.accesstoken))
                        {
                            if (HttpContext.Request.Cookies.ContainsKey("uname"))
                            {
                                Response.Cookies.Delete("uname");
                            }
                            if (HttpContext.Request.Cookies.ContainsKey("pword"))
                            {
                                Response.Cookies.Delete("pword");
                            }

                            if (user.RememberMe == true)
                            {
                                CookieOptions cookieOptions = new CookieOptions();
                                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddHours(12));
                                //Create a cookie to save Login Infomation
                                Response.Cookies.Append("uname", _cryptography.Encrypt(user.Username), cookieOptions);
                                Response.Cookies.Append("pword", _cryptography.Encrypt(user.PasswordHash), cookieOptions);
                            }
                            HttpContext.Session.SetString("JWToken", result.accesstoken);
                            HttpContext.Session.SetString("EncryptedEmail", user.Username);

                            return RedirectToLocal(user.ReturnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid login attempt.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex.Message);

                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
            }
            return View(user);
        }

        [AllowAnonymous]
        public async Task<ActionResult> LogOff()
        {
            Response.Cookies.Delete("uname");
            Response.Cookies.Delete("pword");
            var encryptedEmail = SessionHelper.DoesUserEmailExist(HttpContext.Session, "EncryptedEmail");

            if (!string.IsNullOrWhiteSpace(encryptedEmail))
            {
                await _userService.LogOut(encryptedEmail);
                HttpContext.Session.SetString("EncryptedEmail", string.Empty);
            }
            var token = SessionHelper.DoesTokenExist(HttpContext.Session, "JWToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                HttpContext.Session.SetString("JWToken", string.Empty);
            }

            return RedirectToAction("Login", "User");
        }
        [Authorize]
        public async Task<ActionResult> Keepalive()
        {
            //var encryptedEmail = SessionHelper.DoesUserEmailExist(HttpContext.Session, "EncryptedEmail");

            //if (!string.IsNullOrWhiteSpace(encryptedEmail))
            //{
            //   var token =  await _userService.RefreshToken(encryptedEmail);
            //    if (!string.IsNullOrWhiteSpace(token))
            //    {
            //        HttpContext.Session.SetString("JWToken", string.Empty);
            //    }
            //    HttpContext.Session.SetString("JWToken", token);

            //    var exist = SessionHelper.DoesTokenExist(HttpContext.Session, "JWToken");
          
            //}
     

            return RedirectToAction("Login", "User");
        }


        public async Task RemoveCookiesandTokenAsync()
        {
            Response.Cookies.Delete("uname");
            Response.Cookies.Delete("pword");
            var token = SessionHelper.DoesTokenExist(HttpContext.Session, "JWToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                HttpContext.Session.SetString("JWToken", string.Empty);
            }
            var encryptedEmail = SessionHelper.DoesUserEmailExist(HttpContext.Session, "EncryptedEmail");
            if (!string.IsNullOrWhiteSpace(encryptedEmail))
            {
                await _userService.LogOut(encryptedEmail);
                HttpContext.Session.SetString("EncryptedEmail", string.Empty);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");

        }
    }
}
