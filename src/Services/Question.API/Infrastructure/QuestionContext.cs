using Microsoft.EntityFrameworkCore;
using Question.API.Models;

namespace Question.API.Infrastructure
{
    public class QuestionContext : DbContext
    {

        public QuestionContext(DbContextOptions<QuestionContext> options) : base(options)
        {
        }

        public DbSet<QuestionDetail> Questions { get; set; }
        public DbSet<ChoiceDetail> Choices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionDetail>()
                .ToTable("Questions")
                .HasKey(x => x.Id);

            modelBuilder.Entity<QuestionDetail>()
                .Property(x => x.Id);

            modelBuilder.Entity<QuestionDetail>()
                .HasMany(x => x.Choices)
                .WithOne(y => y.Question);
    

            modelBuilder.Entity<ChoiceDetail>()
                .ToTable("Choices")
                .HasKey(c => new { c.Id, c.QuestionId });

            modelBuilder.Entity<ChoiceDetail>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

        }
    }
}
