using GlobalBilgiQuiz.Data.POCO;
using Microsoft.EntityFrameworkCore;


namespace GlobalBilgiQuiz.Database.DbContexts
{
    public partial class GlobalBilgiQuizDbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
