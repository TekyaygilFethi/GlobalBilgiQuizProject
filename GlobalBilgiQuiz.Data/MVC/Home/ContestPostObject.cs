using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Data.MVC.Home
{
    public class ContestPostObject
    {
        public int QuestionMetricId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int Random1 { get; set; }
        public int Random2 { get; set; }
        public int UserEnteredSumCaptcha { get; set; }
    }
}
