using Express.Interface;
using Express.Interfaces;
using Express.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Express.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWebAPIController : ControllerBase
    {
        private readonly IUserRepository _userrepo;
        private readonly IJwtAuthManager _jwtAuthManager;
        BusinessLayer.Cryptography _cryptography = new BusinessLayer.Cryptography();

        public UserWebAPIController(IUserRepository userrepo, IJwtAuthManager jwtAuthManager)
        {
            _userrepo = userrepo;
            _jwtAuthManager = jwtAuthManager;
        }
        [HttpPost("/api/user/generateaccesstoken")]
        public ActionResult<Models.SignInResult> GenerateAccessToken(Authenticate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var valid = _userrepo.CheckLogin(user);
            if (!valid)
            {
                return Unauthorized();
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(user.Username))
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Username), };

                    var jwtResult = _jwtAuthManager.GenerateTokens(user.Username, claims, DateTime.Now);

                    return Ok(new Models.SignInResult
                    {
                        email = user.Username,
                        accesstoken = jwtResult.AccessToken,
                        refreshtoken = jwtResult.RefreshToken.TokenString
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error allocating access token");
            }
        }
        [HttpGet("/api/user/getuserbyemail/{username}")]
        public ActionResult<User> GetUser(string username)
        {
            try
            {
                var user = _userrepo.GetUserByEmail(username);
                if (user != null && user.Id > 0)
                {
                    return Ok(user);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error getting user account");
            }
        }

        [HttpPost("/api/user/authenticate")]
        public ActionResult<Models.SignInResult> Authenticate(Authenticate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var valid = _userrepo.UserSignIn(user);
            if (!valid)
            {
                return Unauthorized();
            }
            try
            {
                user.Username = _cryptography.Decrypt(user.Username);
                if (!string.IsNullOrWhiteSpace(user.Username))
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Username), };

                    var jwtResult = _jwtAuthManager.GenerateTokens(user.Username, claims, DateTime.Now);

                    return Ok(new Models.SignInResult
                    {
                        email = user.Username,
                        accesstoken = jwtResult.AccessToken,
                        refreshtoken = jwtResult.RefreshToken.TokenString
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error allocating access token");
            }
        }

        [HttpPost("/api/user/logout")]
        [Authorize]
        public ActionResult Logout([FromBody] string email)
        {
            try
            {
                var decrypted = _cryptography.Decrypt(email);
                _jwtAuthManager.RemoveRefreshTokenByUserName(email);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Error updating password");
            }
        }

        [HttpPost("/api/user/refreshtoken")]
        [Authorize]
        public ActionResult RefreshToken(Models.SignInResult signinresult)
        {
            try
            {
                _jwtAuthManager.Refresh(signinresult.refreshtoken, signinresult.accesstoken, DateTime.Now);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Error updating password");
            }
        }
    }
}
