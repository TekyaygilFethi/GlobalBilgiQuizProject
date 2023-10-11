using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Data.Services.QuizServiceFolder
{
    public class ContestObject
    {
        public int QuestionId { get; set; }
        public int QuestionMetricId { get; set; }
        public int QuestionOrder { get; set; }
        public string QuestionText { get; set; }

        public IEnumerable<ContestObjectAnswer> ShowcaseAnswers { get; set; }
    }
}
