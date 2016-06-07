using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Areas.Member.Models;
using CMS.BLL;
using CMS.Common;
using CMS.IBLL;
using CMS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace CMS.Areas.Member.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private InterfaceUserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Regist()
        {
            return View();
        }

        [HttpPost]
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "注册失败！");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var _user = userService.Find(model.UserName);
                if (_user == null)
                {
                    ModelState.AddModelError("UserName", "用户名不存在");
                    return View(model);
                }
                else if (_user.Password == Common.Security.Sha256(model.Password))
                {
                    _user.LoginTime = DateTime.Now;
                    _user.LoginIP = Request.UserHostAddress;
                    userService.Update(_user);

                    var _identity = userService.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = model.RemenberMe
                    }, _identity);
                    if(string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    else if(Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
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

        [AllowAnonymous]
        public ActionResult VerificationCode()
        {
            string code = Security.CreateVerification(6);
            Bitmap img = Security.CreateVerificationImage(code, 160, 30);
            img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            TempData["VerificationCode"] = code.ToUpper();
            return null;
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View(userService.Find(User.Identity.Name));
        }

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Modify()
        {
            var user = userService.Find(User.Identity.Name);
            if(user == null)
                ModelState.AddModelError("","用户不存在");
            else
            {
                if (TryUpdateModel(user, new string[] {"Email"}))   //表示只想从客户提交的数据中更新Email
                {
                    if (ModelState.IsValid)
                    {
                        if(userService.Update(user))
                            ModelState.AddModelError("","修改成功");
                        else
                            ModelState.AddModelError("","修改失败");
                    }
                }
                else
                    ModelState.AddModelError("","更新数据模型失败");
            }

            return View("Details", user);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userService.Find(User.Identity.Name);
                if (user.Password == Common.Security.Sha256(model.OriginalPassword))
                {
                    user.Password = Common.Security.Sha256(model.Password);
                    if (userService.Update(user))
                        ModelState.AddModelError("", "修改密码成功");
                    else
                        ModelState.AddModelError("", "密码修改失败");
                }
                else
                    ModelState.AddModelError("", "原始密码错误");
            }

            return View(model);
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