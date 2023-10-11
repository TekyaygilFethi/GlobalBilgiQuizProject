using GlobalBilgiQuiz.Data.Base.Classes;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalBilgiQuiz.Data.POCO
{
    [Table("Answers")]
    public class Answer : PrimaryKeyEntity<int>
    {
        public string Text { get; set; }

        public Question Question { get; set; }

        public int QuestionId { get; set; }

        public bool IsTrue { get; set; }

        public int ChosenCount { get; set; }
    }

}
