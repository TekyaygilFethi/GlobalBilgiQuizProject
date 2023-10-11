using GlobalBilgiQuiz.Data.POCO;
using GlobalBilgiQuiz.Database.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        GlobalBilgiQuizDbContext _ctx;

        public Repository(GlobalBilgiQuizDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Delete(T item)
        {
            _ctx.Set<T>().Remove(item);
        }

        public void Delete(List<T> items)
        {
            _ctx.Set<T>().RemoveRange(items);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _ctx.Set<T>().Any();
        }

        public List<T> GetAll(bool isReadOnly = false, List<string>? includeExpressions = null)
        {
            var query = _ctx.Set<T>().AsQueryable();

            if (isReadOnly)
                query = query.AsNoTracking();

            if (includeExpressions != null && includeExpressions.Any())
            {
                foreach (var exp in includeExpressions)
                    query = query.Include(exp);
            }

            return query.ToList();
        }

        public IQueryable<T> GetAllQueryable(bool isReadOnly = false, List<string>? includeExpressions = null)
        {
            var query = _ctx.Set<T>().AsQueryable();

            if (isReadOnly)
                query = query.AsNoTracking();

            if (includeExpressions != null && includeExpressions.Any())
            {
                foreach (var exp in includeExpressions)
                    query = query.Include(exp);
            }

            return query;
        }

        public T? GetSingle(Expression<Func<T, bool>> predicate, bool isReadOnly = false, List<string>? includeExpressions = null)
        {
            var query = _ctx.Set<T>().AsQueryable();

            if (isReadOnly)
                query = query.AsNoTracking();

            if (includeExpressions != null && includeExpressions.Any())
            {
                foreach (var exp in includeExpressions)
                    query = query.Include(exp);
            }

            return query.SingleOrDefault(predicate);
        }

        public void Insert(T item)
        {
            _ctx.Set<T>().Add(item);
        }

        public void Insert(List<T> items)
        {
            _ctx.Set<T>().AddRange(items);
        }
    }
}
