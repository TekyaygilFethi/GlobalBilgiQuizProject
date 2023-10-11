using GlobalBilgiQuiz.Business.Repositories;
using GlobalBilgiQuiz.Database.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.UnitOfWorkFolder
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly GlobalBilgiQuizDbContext _ctx;
        public UnitOfWork(GlobalBilgiQuizDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool SaveChanges()
        {
            using var ctxTransaction = _ctx.Database.BeginTransaction();
            try
            {
                _ctx.SaveChanges();
                ctxTransaction.Commit();
                return true;
            }
            catch(Exception)
            {
                ctxTransaction.Rollback();
                return false;
            }
        }

        public IRepository<T>? GetRepository<T>() where T : class 
        {
            return (IRepository<T>?)Activator.CreateInstance(typeof(Repository<T>), new object[] { _ctx });
        }

        public void Dispose()
        {
            
        }
    }
}
