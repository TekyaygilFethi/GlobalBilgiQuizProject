using GlobalBilgiQuiz.Data.POCO;
using Microsoft.EntityFrameworkCore;

namespace GlobalBilgiQuiz.Database.DbContexts
{
    public partial class GlobalBilgiQuizDbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<Question> Questions { get; set; }

        private void ConfigureQuizEntities(ModelBuilder builder)
        {
            builder.ConfigureQuizBuilder();
            builder.Seed();
        }
    }
}
