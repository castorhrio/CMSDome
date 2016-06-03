using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.BLL;
using CMS.Common;
using CMS.IBLL;
using CMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace CMS.Controllers
{
    public class UserController : Controller
    {
        private InterfaceUserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        // GET: User
        public ActionResult Regist()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Regist(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (TempData["VerificationCode"] == null ||
                    TempData["VerificationCode"].ToString() != model.VerificationCode.ToUpper())
                {
                    ModelState.AddModelError("VerificationCode", "验证码不正确");
                    return View(model);
                }

                if (userService.Exist(model.UserName))
                {
                    ModelState.AddModelError("UserName", "用户名已存在");
                    return View(model);
                }
                User user = new User()
                {
                    UserID = Guid.NewGuid(),
                    UserName = model.UserName,
                    Password = Security.Sha256(model.PassWord),
                    Email = model.Email,
                    Status = 0,
                    RegistTime = DateTime.Now
                };

                var result = userService.Add(user);
                if (result != null)
                {
                    var _identity = userService.CreateIdentity(result, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(_identity);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "注册失败！");
                }
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _user = userService.Find(model.UserName);
                if (_user == null)
                {
                    ModelState.AddModelError("UserName","用户名不存在");
                    return View(model);
                }
                else if(_user.Password == Common.Security.Sha256(model.Password))
                {
                    _user.LoginTime = DateTime.Now;
                    _user.LoginIP = Request.UserHostAddress;
                    userService.Update(_user);

                    var _identity = userService.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = model.RemenberMe
                    },_identity);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "密码错误");
                }
            }

            return View();
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect(Url.Content("~/"));
        }

        public ActionResult VerificationCode()
        {
            string code = Security.CreateVerification(6);
            Bitmap img = Security.CreateVerificationImage(code, 160, 30);
            img.Save(Response.OutputStream,System.Drawing.Imaging.ImageFormat.Jpeg);
            TempData["VerificationCode"] = code.ToUpper();
            return null;
        }

        #region 属性

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion
    }
}