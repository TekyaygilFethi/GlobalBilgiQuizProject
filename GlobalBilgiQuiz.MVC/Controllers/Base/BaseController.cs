using GlobalBilgiQuiz.Business.UnitOfWorkFolder;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBilgiQuiz.MVC.Controllers.Base
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _uow;
        protected BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected bool Save()
        {
            return _uow.SaveChanges();
        }
    }
}
