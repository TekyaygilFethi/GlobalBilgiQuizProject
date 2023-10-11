using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GlobalBilgiQuiz.MVC.Filters
{
    public class EndContestFilter:ActionFilterAttribute, IActionFilter
    {
        private string _filePath = string.Empty;

        public EndContestFilter(IWebHostEnvironment environment)
        {
            _filePath = Path.Combine(environment.WebRootPath, "files", "CurrentQuestion.txt");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int currentQuestionOrder = int.Parse(File.ReadAllText(_filePath));
            if (currentQuestionOrder > 3)
            {
                context.Result = new RedirectToActionResult("EndContest", "Home", null);
            }
        }
    }
}
