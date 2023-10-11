using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.UnitOfWorkFolder
{
    public interface IUnitOfWork
    {
        bool SaveChanges();
    }
}
