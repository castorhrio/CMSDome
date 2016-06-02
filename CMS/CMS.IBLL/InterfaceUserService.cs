using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models;

namespace CMS.IBLL
{
    public interface InterfaceUserService:InterfaceBaseService<User>
    {
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool Exist(string userName);

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        User Find(int UserId);

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        User Find(string UserName);

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pageIndex">页码数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="order">排序：0-ID升序（默认），1ID降序，2注册时间升序，3注册时间降序，4登录时间升序，5登录时间降序</param>
        /// <returns></returns>
        IQueryable<User> FindPageList(int pageIndex, int pageSize, out int totalRecord, int order);
    }
}
