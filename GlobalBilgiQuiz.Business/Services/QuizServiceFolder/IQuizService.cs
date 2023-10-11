using GlobalBilgiQuiz.Data.Services.QuizServiceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Business.Services.QuizServiceFolder
{
    public interface IQuizService
    {
        ContestObject GetQuestionObject(int currentQuestionOrder);
        void AnswerQuestion(AnswerQuestionModel model);

        TrueFalseCount GetTotalStatistics();

        IEnumerable<int> GetChosenCounts(int currentQuestionOrder);

    }
}
