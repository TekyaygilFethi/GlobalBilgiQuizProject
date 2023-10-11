using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Database.DbContexts
{
    public partial class GlobalBilgiQuizDbContext : DbContext
    {
        public GlobalBilgiQuizDbContext(DbContextOptions<GlobalBilgiQuizDbContext> options): base(options)
        {
        }

        public GlobalBilgiQuizDbContext():base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureQuizEntities(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.EnableSensitiveDataLogging();
        }

    }
}
