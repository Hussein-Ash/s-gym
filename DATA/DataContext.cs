using EvaluationBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluationBackend.DATA
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<AppUser> Users { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionInfo> SubscriptionsInfo { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<DayExercise> DayExercises { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            AppUser[] u = {
                new() {
                    CreationDate=DateTime.UtcNow,
                    FullName="Super",
                    UserName="SuperAdmin",
                    Role=UserRole.SuperAdmin,
                    Password="12345678",
                    PhoneNumber="07816565518",
                    Id=Guid.Parse("0f8f8a71-fa93-4897-7a54-45a069619c0e")

                }

            };
            AppUser[] y = {
                new() {
                    CreationDate=DateTime.UtcNow,
                    FullName="Admin",
                    UserName="Admin",
                    Role=UserRole.Admin,
                    Password="12345678",
                    PhoneNumber="07816565518",
                    Id=Guid.Parse("0f8f8a71-fa93-4897-7a54-43a069619c0e")

                }

            };




            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.Sub)
                .WithOne(s => s.User)
                .HasForeignKey<AppUser>(a => a.SubId);

            modelBuilder.Entity<Section>()
                .HasMany(s => s.CourseName)
                .WithOne(sub => sub.SectionName)
                .HasForeignKey(s => s.SectionId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Section>()
                .HasMany(s => s.Subscriptions)
                .WithOne(sub => sub.SectionName)
                .HasForeignKey(sub => sub.SectionId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Day>()
                .HasOne(d => d.CourseName)
                .WithMany(c => c.Days)
                .HasForeignKey(d => d.CourseId);


            modelBuilder.Entity<Subscription>()
                .HasOne(sub => sub.User)
                .WithOne(u => u.Sub)
                .HasForeignKey<Subscription>(sub => sub.UserId);

            modelBuilder.Entity<Subscription>()
                .HasOne(sub => sub.SubInfo)
                .WithOne(si => si.Sub)
                .HasForeignKey<Subscription>(sub => sub.SubInfoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubscriptionInfo>()
                .HasOne(si => si.Sub)
                .WithOne(s => s.SubInfo)
                .HasForeignKey<SubscriptionInfo>(si => si.SubId);

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Recipient)
                .WithMany(x => x.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany(x => x.MessagesSent)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Notification>()
                .HasOne(x => x.User)
                .WithMany(x => x.NotificationsReceived)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Notification>()
                .HasOne(x => x.Sender)
                .WithMany(x => x.NotificationsSent)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasMany(s => s.Days)
                .WithOne(sub => sub.CourseName)
                .HasForeignKey(sub => sub.CourseId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<AppUser>().HasData(u);
            modelBuilder.Entity<AppUser>().HasData(y);



            //     // Configure JsonData to be ignored by EF Core's entity mapping
            //     entity.Property(e => e.JsonObject)
            //           .HasConversion(
            //               v => v.ToString(),   // Convert JObject to string for storage
            //               v => JObject.Parse(v));  // Convert string back to JObject
            // });


            // seed data 

        }

    }
}