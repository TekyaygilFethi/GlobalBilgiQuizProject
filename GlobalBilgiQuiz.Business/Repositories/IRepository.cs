using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.Repositories
{
    public interface IRepository<T> where T : class
    {
        // CRUD

        #region Create
        void Insert(T item);

        void Insert(List<T> items);

        #endregion

        #region Read
        IQueryable<T> GetAllQueryable(bool isReadOnly = false, List<string>? includeExpressions = null);

        T? GetSingle(Expression<Func<T, bool>> predicate, bool isReadOnly = false, List<string>? includeExpressions = null);

        List<T> GetAll(bool isReadOnly = false, List<string>? includeExpressions = null);

        bool Exists(Expression<Func<T, bool>> predicate);
        #endregion

        #region Delete
        void Delete(T item);
        void Delete(List<T> items);
        #endregion


    }
}
