using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CMS.IDAL;

namespace CMSDAL
{
    public class BaseRepository<T>:InterfaceBaseRepository<T> where T:class
    {
        protected CMSDbContext dbContext = ContextFactory.GetCurrentContext();

        public T Add(T entity)
        {
            dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            dbContext.SaveChanges();
            return entity;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Count(predicate);
        }

        public bool Update(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return dbContext.SaveChanges() > 0;
        }

        public bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return dbContext.Set<T>().Any(anyLambda);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = dbContext.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        public IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba, bool isAsc, string orderName)
        {
            var _list = dbContext.Set<T>().Where<T>(whereLamdba);
            _list = OrderBy(_list, orderName, isAsc);
            return _list;
        }

        public IQueryable<T> FindPageList(int pageIndex, int pageSize, out int totalRecord,
            Expression<Func<T, bool>> whereLamdba, bool isAsc, string orderName)
        {
            var _list = dbContext.Set<T>().Where<T>(whereLamdba);
            totalRecord = _list.Count();
            _list = OrderBy(_list, orderName, isAsc).Skip<T>((pageIndex - 1)*pageSize).Take<T>(pageSize);
            return _list;
        }

        private IQueryable<T> OrderBy(IQueryable<T> source, string propertyName, bool isAsc)
        {
            if(source == null)
                throw new ArgumentException("source","不能为空");
            if(string.IsNullOrEmpty(propertyName))
                return source;
            var _parameter = Expression.Parameter(source.ElementType);
            var _property = Expression.Property(_parameter, propertyName);
            if(_property == null)
                throw new ArgumentNullException("propertyName","属性不存在");
            var _lambda = Expression.Lambda(_property, _parameter);
            var _methodName = isAsc ? "OrderBy" : "OrderByDescending";
            var _resultExpression = Expression.Call(typeof (Queryable), _methodName, new Type[]
            {
                source.ElementType, _property.Type
            }, source.Expression, Expression.Quote(_lambda));
            return source.Provider.CreateQuery<T>(_resultExpression);
        } 
    }
}
