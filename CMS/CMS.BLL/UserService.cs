using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CMS.Models;
using CMS.IBLL;
using CMSDAL;
using Microsoft.AspNet.Identity;

namespace CMS.BLL
{
    public class UserService:BaseService<User>, InterfaceUserService
    {
        public UserService() : base(RepositoryFactory.UserRepository) { }

        public bool Exist(string UserName)
        {
            return CurrentRepository.Exist(u => u.UserName == UserName);
        }

        public User Find(Guid UserId)
        {
            return CurrentRepository.Find(u => u.UserID == UserId);
        }

        public User Find(string userName)
        {
            return CurrentRepository.Find(u => u.UserName == userName);
        }

        public IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order)
        {
            bool _isAsc = true;
            string _orderName = string.Empty;
            switch (order)
            {
                case 0:
                    _isAsc = true;
                    _orderName = "UserID";
                    break;
                case 1:
                    _isAsc = false;
                    _orderName = "UserID";
                    break;
                case 2:
                    _isAsc = true;
                    _orderName = "RegistTime";
                    break;
                case 3:
                    _isAsc = false;
                    _orderName = "RegistTime";
                    break;
                case 4:
                    _isAsc = true;
                    _orderName = "LoginTime";
                    break;
                case 5:
                    _isAsc = false;
                    _orderName = "LoginTime";
                    break;
                default:
                    _isAsc = false;
                    _orderName = "UserID";
                    break;
            }

            return CurrentRepository.FindPageList(pageIndex, pageSize, out totalRecord, u => true, _isAsc, _orderName);
        }

        public ClaimsIdentity CreateIdentity(User user, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name,user.UserName));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()));
            _identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));

            return _identity;
        }
    }
}
