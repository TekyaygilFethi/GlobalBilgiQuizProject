using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Data.MVC.Home
{
    public class ChosenCountObject
    {
        public IEnumerable<int> ChosenCounts { get; set; }

        public int CurrentQuestionOrder { get; set; }
    }
}
