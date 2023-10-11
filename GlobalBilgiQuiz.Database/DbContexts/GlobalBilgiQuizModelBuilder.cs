using GlobalBilgiQuiz.Data.POCO;
using GlobalBilgiQuiz.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBilgiQuiz.Database.DbContexts
{
    public static class GlobalBilgiQuizModelBuilder
    {
        public static void ConfigureQuizBuilder(this ModelBuilder builder)
        {
            builder.Entity<Answer>()
                .HasOne(_ => _.Question)
                .WithMany(_ => _.Answers)
                .HasForeignKey(fk => fk.QuestionId);

            builder.Entity<Metric>()
                .HasOne(_ => _.Question)
                .WithOne(_ => _.Metric)
                .HasForeignKey<Metric>(fk => fk.QuestionId);

            builder.HasSequence<int>("OrderSequence")
                .StartsAt(4).IncrementsBy(1);

            builder.Entity<Question>()
                .Property(_ => _.Order)
                .HasDefaultValueSql("NEXT VALUE FOR OrderSequence");
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Question 1
            Question q1 = new Question
            {
                Id = 1,
                Text = "Mustafa Kemal Atatürk kaç yılınca doğmuştur?",
                Order = 1
            };

            Metric q1m = new Metric { Id = 1, QuestionId = 1 };

            Answer q1a1 = new Answer
            {
                Id = 1,
                Text = "1881",
                IsTrue = true,
                QuestionId = 1
            };

            Answer q1a2 = new Answer
            {
                Id = 2,
                Text = "1991",
                IsTrue = false,
                QuestionId = 1
            };

            Answer q1a3 = new Answer
            {
                Id = 3,
                Text = "1923",
                IsTrue = false,
                QuestionId = 1
            };

            Answer q1a4 = new Answer
            {
                Id = 4,
                Text = "1900",
                IsTrue = false,
                QuestionId = 1
            };

            modelBuilder.Entity<Question>().HasData(q1);
            modelBuilder.Entity<Metric>().HasData(q1m);
            modelBuilder.Entity<Answer>().HasData(q1a1, q1a2, q1a3, q1a4);

            #endregion

            #region Question 2
            Question q2 = new Question
            {
                Id = 2,
                Text = "Hangisi 2. Dünya Savaşı döneminde yaşamıştır?",
                Order = 2
            };

            Metric q2m = new Metric { Id = 2, QuestionId = 2 };

            Answer q2a1 = new Answer
            {
                Id = 5,
                Text = "Seda Sayan",
                IsTrue = false,
                QuestionId = 2
            };

            Answer q2a2 = new Answer
            {
                Id = 6,
                Text = "İbrahim Tatlıses",
                IsTrue = false,
                QuestionId = 2
            };

            Answer q2a3 = new Answer
            {
                Id = 7,
                Text = "Winston Churcill",
                IsTrue = true,
                QuestionId = 2
            };

            Answer q2a4 = new Answer
            {
                Id = 8,
                Text = "Türkcan Keskin",
                IsTrue = false,
                QuestionId = 2
            };

            modelBuilder.Entity<Question>().HasData(q2);
            modelBuilder.Entity<Metric>().HasData(q2m);
            modelBuilder.Entity<Answer>().HasData(q2a1, q2a2, q2a3, q2a4);

            #endregion

            #region Question 3
            Question q3 = new Question
            {
                Id = 3,
                Text = "Futbolda geçen senenin şampiyonu kim oldu?",
                Order = 3
            };

            Metric q3m = new Metric { Id = 3, QuestionId = 3 };

            Answer q3a1 = new Answer
            {
                Id = 9,
                Text = "Beşiktaş",
                IsTrue = false,
                QuestionId = 3
            };

            Answer q3a2 = new Answer
            {
                Id = 10,
                Text = "Fenerbahçe",
                IsTrue = false,
                QuestionId = 3
            };

            Answer q3a3 = new Answer
            {
                Id = 11,
                Text = "Trabzonspor",
                IsTrue = false,
                QuestionId = 3
            };

            Answer q3a4 = new Answer
            {
                Id = 12,
                Text = "Galatasaray",
                IsTrue = true,
                QuestionId = 3
            };

            modelBuilder.Entity<Question>().HasData(q3);
            modelBuilder.Entity<Metric>().HasData(q3m);
            modelBuilder.Entity<Answer>().HasData(q3a1, q3a2, q3a3, q3a4);

            #endregion

            User user = new User
            {
                Id = 1,
                Username = "Admin",
                Password = CryptographyHelper.Encode("1234", "LnKpsvEmzRBc9fS")
            };

            modelBuilder.Entity<User>().HasData(user);
        }
    }
}
