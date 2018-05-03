using Doggies.Controllers.Abstract;
using Doggies.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Doggies.Controllers.Api
{
    [NotRedirectWebApiAuthorize]
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return WrapError("Логин / пароль не введены.");
                //return BadRequest(ModelState);
            }

            // если логин содержит собаку, то проверяем, как email
            if (model.Login.Contains("@"))
            {
                User emailUser = await UserManager.FindByEmailAsync(model.Login);

                if (emailUser != null)
                {
                    model.Login = emailUser.UserName;
                }
            }

            User user = await UserManager.FindAsync(model.Login, model.Password);

            if (user == null)
            {
                return WrapError("Логин / пароль введены не верно.");
            }
            
            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            AuthManager.SignOut();

            AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, ident);
            
            return WrapSuccess(await CreateUserProfile(user));
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            User user = new User() { UserName = model.Login,  Email = model.Email };

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInAsync(user, false);
                return WrapSuccess(await CreateUserProfile(user));
            }
            else return WrapError(String.Join(". ", result.Errors));
        }

        public async Task SignInAsync(User user, bool isPersistent)
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignIn(identity);
        }

        [HttpPost]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            AuthManager.SignOut();
            return WrapSuccess(null);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            //if (!AuthManager.User.Identity.IsAuthenticated) SetResponseMessage(ApiResponseWrap.MessageType.success, "Пользователь не авторизован");
            if (AuthManager.User.Identity.IsAuthenticated)
            {
                var a = await UserManager.FindByNameAsync(AuthManager.User.Identity.Name);
                var b = await CreateUserProfile(a);
                return WrapSuccess(b);
            }
            else return WrapSuccess(null);
        }

        private async Task<UserProfile> CreateUserProfile(User user)
        {
            if (user != null)
            {
                UserProfile profile = new UserProfile(user);
                return profile;
            }

            return null;
        }

    }
    
}