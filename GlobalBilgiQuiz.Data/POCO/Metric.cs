using GlobalBilgiQuiz.Data.Base.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalBilgiQuiz.Data.POCO
{
    [Table("Metrics")]
    public class Metric : PrimaryKeyEntity<int>
    {
        public int TrueCount { get; set; }
        public int FalseCount { get; set; }

        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
