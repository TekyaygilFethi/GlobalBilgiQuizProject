using GlobalBilgiQuiz.Data.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalBilgiQuiz.Data.Base.Classes
{
    public class PrimaryKeyEntity<T> : IPrimaryKey<T>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}
