using GlobalBilgiQuiz.Business.Services.AdminServiceFolder;
using GlobalBilgiQuiz.Business.Services.CacheServiceFolder;
using GlobalBilgiQuiz.Business.SignalRHubs;
using GlobalBilgiQuiz.Business.UnitOfWorkFolder;
using GlobalBilgiQuiz.Data.Services.AdminServiceFolder;
using GlobalBilgiQuiz.MVC.Controllers.Base;
using GlobalBilgiQuiz.MVC.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.CodeDom;
using System.Runtime.CompilerServices;

namespace GlobalBilgiQuiz.MVC.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly string _counterDateFilePath = string.Empty;
        private readonly string _currentQuestionFilePath = string.Empty;
        private IHubContext<QuizHub> _quizHubContext;
        private ICacheService _cacheService;


        public AdminController(IAdminService adminService, IWebHostEnvironment environment, IHubContext<QuizHub> quizHubContext, ICacheService cacheService, IUnitOfWork uow) : base(uow)
        {
            _adminService = adminService;
            _counterDateFilePath = Path.Combine(environment.WebRootPath, "files", "CounterDateFile.txt");
            _currentQuestionFilePath = Path.Combine(environment.WebRootPath, "files", "CurrentQuestion.txt");
            _quizHubContext = quizHubContext;
            _cacheService = cacheService;
        }

        public IActionResult Start()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Start(UserDTO userDTO)
        {
            if(_adminService.CheckUser(userDTO))
            {
                HttpContext.Session.SetString("User", "Admin");
                return RedirectToAction("StartContest", "Admin");
            }
            TempData["ErrorMessage"] = "Kullanıcı Adı ve Şifre Hatalı!";
            return View();
        }


        [ServiceFilter(typeof(AdminFilter))]
        public IActionResult StartContest()
        {
            return View();
        }
        
        [ServiceFilter(typeof(AdminFilter)), HttpPost]
        public async Task<IActionResult> StartContestPost()
        {
            _cacheService.Set("RemainingTime", DateTime.Now.AddSeconds(32).ToString("dd.MM.yyyy HH:mm:ss"), DateTime.Now.AddMinutes(1));

            var currentQuestion = _cacheService.Get<int?>("CurrentQuestion");
            if(currentQuestion == null || currentQuestion == 0)
            {
                _cacheService.Set("CurrentQuestion", 1, DateTime.Now.AddHours(1));

            }

            await _quizHubContext.Clients.All.SendAsync("startcontest");
            return RedirectToAction("AdminContest");
        }


        public IActionResult AdminContest()
        {
            return View();
        }

        public async Task<JsonResult> ChangeQuestion(string isIncrement = "true")
        {
            int currentQuestion = int.Parse(System.IO.File.ReadAllText(_currentQuestionFilePath));

            if (isIncrement == "true")
            {
                currentQuestion += 1;
            }

            System.IO.File.WriteAllText(_currentQuestionFilePath, currentQuestion.ToString());

            if(currentQuestion > 3)
            {
                return Json(new { Success = "true", data = "redirect" });
            }

            await _quizHubContext.Clients.All.SendAsync("resetsinglebarchart");
            await _quizHubContext.Clients.All.SendAsync("changequestion");

            var question = _adminService.GetCurrentQuestion(currentQuestion);

            System.IO.File.WriteAllText(_counterDateFilePath, DateTime.Now.AddSeconds(32).ToString("dd.MM.yyyy HH:mm:ss"));

            return Json(new { Success = "true", data = question });
        }
    }
}
