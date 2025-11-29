using CoachBuddy.Domain.Entities.Client;
using CoachBuddy.Domain.Entities.Exercise;
using CoachBuddy.Domain.Entities.Group;
using CoachBuddy.Domain.Entities.TrainingPlan;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoachBuddy.Infrastructure.Persistence
{
    public class CoachBuddyDbContext : IdentityDbContext
    {
        public CoachBuddyDbContext(DbContextOptions<CoachBuddyDbContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientTraining> Trainings { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ClientGroup> ClientGroups { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<TrainingPlanExercise> TrainingPlanExercises { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .OwnsOne(c => c.ContactDetails);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Trainings)
                .WithOne(s => s.Client)
                .HasForeignKey(s => s.ClientId);

            modelBuilder.Entity<ClientGroup>()
                .HasKey(cg => new { cg.ClientId, cg.GroupId });

            modelBuilder.Entity<ClientGroup>()
                .HasOne(cg => cg.Client)
                .WithMany(c => c.ClientGroups)
                .HasForeignKey(cg => cg.ClientId);

            modelBuilder.Entity<ClientGroup>()
                .HasOne(cg => cg.Group)
                .WithMany(g => g.ClientGroups)
                .HasForeignKey(cg => cg.GroupId);

            modelBuilder.Entity<Exercise>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<TrainingPlan>()
                .HasMany(tp => tp.Groups)
                .WithMany(g => g.TrainingPlans);

            modelBuilder.Entity<TrainingPlan>()
                .HasMany(tp => tp.TrainingPlanExercises)
                .WithOne(tpe => tpe.TrainingPlan)
                .HasForeignKey(tpe => tpe.TrainingPlanId);

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.TrainingPlanExercises)
                .WithOne(tpe => tpe.Exercise)
                .HasForeignKey(tpe => tpe.ExerciseId);

            modelBuilder.Entity<TrainingPlanExercise>()
                .HasOne(e => e.Exercise)
                .WithMany(e => e.TrainingPlanExercises)
                .HasForeignKey(e => e.ExerciseId);

        }
    }
}

