using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.IDAL
{
    /// <summary>
    /// 接口基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface InterfaceBaseRepository<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        T Add(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(T entity);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="anyLamdba">查询表达式</param>
        /// <returns></returns>
        bool Exist(Expression<Func<T, bool>> anyLamdba);

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="whereLamdba"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> whereLamdba);

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="whereLamdba"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderLamdba">排序表达式</param>
        /// <returns></returns>
        IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba, bool isAsc,string orderName);

        /// <summary>
        /// 查找分页数据列表
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <param name="whereLamdba"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderLamdba"></param>
        /// <returns></returns>
        IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord,
            Expression<Func<T, bool>> whereLamdba, bool isAsc, string orderName);
    }
}
