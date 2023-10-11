using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GlobalBilgiQuiz.MVC.Filters
{
    public class QuestionPageFilter: ActionFilterAttribute, IActionFilter
    {
        private string _filePath = string.Empty;
        public QuestionPageFilter(IWebHostEnvironment environment)
        {
            _filePath = Path.Combine(environment.WebRootPath, "files","CurrentQuestion.txt");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string currentQuestion = File.ReadAllText(_filePath);
            if(currentQuestion == "0")
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
        }

    }
}
