using GlobalBilgiQuiz.Data.Services.QuizServiceFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Data.SignalR
{
    public class QuestionBaseTrueFalseCount: TrueFalseCount
    {
        public int AnswerAChosenCount { get; set; }
        public int AnswerBChosenCount { get; set; }
        public int AnswerCChosenCount { get; set; }
        public int AnswerDChosenCount { get; set; }
    }
}
