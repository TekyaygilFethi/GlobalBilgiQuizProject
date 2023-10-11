using GlobalBilgiQuiz.Business.Services.QuizServiceFolder;
using GlobalBilgiQuiz.Business.SignalRHubs;
using GlobalBilgiQuiz.Business.UnitOfWorkFolder;
using GlobalBilgiQuiz.Data.MVC.Home;
using GlobalBilgiQuiz.Data.Services.QuizServiceFolder;
using GlobalBilgiQuiz.Database.DbContexts;
using GlobalBilgiQuiz.MVC.Controllers.Base;
using GlobalBilgiQuiz.MVC.Filters;
using GlobalBilgiQuiz.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GlobalBilgiQuiz.MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private string _counterDataFilePath = string.Empty;
        private string _currentQuestionFilePath = string.Empty;
        private IQuizService _quizService;
        private IHubContext<QuizHub> _quizHubContext;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, IQuizService quizService, IHubContext<QuizHub> quizHubContext, IUnitOfWork uow):base(uow)
        {
            _logger = logger;
            _counterDataFilePath = Path.Combine(environment.WebRootPath, "files", "CounterDateFile.txt");
            _currentQuestionFilePath = Path.Combine(environment.WebRootPath, "files", "CurrentQuestion.txt");
            _quizService = quizService;
            _quizHubContext = quizHubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ServiceFilter(typeof(EndContestFilter)), ServiceFilter(typeof(QuestionPageFilter))]
        [Route("yarisma"), ActionName("Contest")]
        public IActionResult Contest()
        {
            int currentQuestionOrder = int.Parse(System.IO.File.ReadAllText(_currentQuestionFilePath));

            var question = _quizService.GetQuestionObject(currentQuestionOrder);

            return View(question);
        }


        [Route("yarismapost")]
        [ServiceFilter(typeof(EndContestFilter)), ServiceFilter(typeof(QuestionPageFilter))]
        [HttpPost]
        public async Task<JsonResult> Contest([FromBody] ContestPostObject cpo)
        {
            if (cpo.Random1 + cpo.Random2 != cpo.UserEnteredSumCaptcha)
                return Json(new { IsSuccess = false });

            _quizService.AnswerQuestion(new AnswerQuestionModel
            {
                ChosenAnswerId = cpo.AnswerId,
                QuestionId = cpo.QuestionId
            });

            base.Save();

            await _quizHubContext.Clients.All.SendAsync("BroadcastByQuestion", new { info = GetChosenCounts() });// hangi şık ne kadar seçildi?
            await _quizHubContext.Clients.All.SendAsync("Broadcast", new { info = GetTotalStatistics() });// kümülatif toplam için doğru yanlış eklendi?


            return Json(new { IsSuccess = true });
        }

        public async Task<JsonResult> GetTimeDifference()
        {
            var newDate = DateTime.ParseExact(System.IO.File.ReadAllText(_counterDataFilePath), "dd.MM.yyyy HH:mm:ss", null);
            var now = DateTime.Now;
            var secondDifference = (newDate - now).Seconds;

            //Signalr UpdateCounter
            await _quizHubContext.Clients.All.SendAsync("UpdateCounter");

            if (secondDifference <= 0)
            {
                await _quizHubContext.Clients.All.SendAsync("EndCountdown");
            }

            return Json(new { seconds = secondDifference });
        }


        [ServiceFilter(typeof(EndContestFilter))]
        [Route("sonuclar")]
        public IActionResult SeeResults()
        {
            return View();
        }

        public JsonResult SeeResultsJson()
        {
            var chosenCounts = GetChosenCounts();

            return Json(new { data = chosenCounts });
        }

        //globalbilgiquiz.com/kapanis
        [Route("kapanis"), ActionName("EndContest")]
        public IActionResult EndContest()
        {
            return View();

        }
        public TrueFalseCount GetTotalStatistics()
        {
            return _quizService.GetTotalStatistics();
        }

        private ChosenCountObject GetChosenCounts()
        {
            int currentQuestionOrder = int.Parse(System.IO.File.ReadAllText(_currentQuestionFilePath));

            var chosenCounts = _quizService.GetChosenCounts(currentQuestionOrder);

            return new ChosenCountObject { ChosenCounts = chosenCounts, CurrentQuestionOrder = currentQuestionOrder };
        }


    }
}