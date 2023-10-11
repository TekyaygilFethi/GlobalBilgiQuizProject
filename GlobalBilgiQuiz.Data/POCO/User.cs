using GlobalBilgiQuiz.Data.Base.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalBilgiQuiz.Data.POCO
{
    [Table("Users")]
    public class User : PrimaryKeyEntity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
