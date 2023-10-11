using GlobalBilgiQuiz.Data.Base.Classes;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalBilgiQuiz.Data.POCO
{
    [Table("Questions")]
    public class Question : PrimaryKeyEntity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }

        public string Text { get; set; }

        public Metric Metric { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
