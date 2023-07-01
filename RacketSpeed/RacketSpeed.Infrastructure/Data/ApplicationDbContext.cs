﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RacketSpeed.Infrastructure.Data.Entities;

namespace RacketSpeed.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<Coach> Coaches { get; set; } = null!;
        public DbSet<Court> Courts { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Training> Trainings { get; set; } = null!;
        public DbSet<PostImageUrl> PostImageUrls { get; set; } = null!;
        public DbSet<PlayerImageUrl> PlayerImageUrls { get; set; } = null!;
        public DbSet<CoachImageUrl> CoachImageUrls { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventImageUrl> EventImageUrls { get; set; } = null!;
        public DbSet<SignKid> SignedKids { get; set; } = null!;
        public DbSet<Schedule> Schedule { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // This might be a problem in the future if any of the entities come with
            // large amount of images.
            // Fluent API to configure a Coach to always come with its ImageUrls.
            builder.Entity<Coach>().Navigation(img => img.CoachImageUrl).AutoInclude();
            // Fluent API to configure a Player to always come with its ImageUrls.
            builder.Entity<Player>().Navigation(img => img.PlayerImageUrl).AutoInclude();
            // Fluent API to configure a Post to always come with its ImageUrls.
            builder.Entity<Post>().Navigation(img => img.PostImageUrls).AutoInclude();
            // Fluent API to configure an Event to always come with its ImageUrls.
            builder.Entity<Event>().Navigation(img => img.EventImageUrls).AutoInclude();

            // Fluent API to configure an Reservation to always come with its User and Court.
            builder.Entity<Reservation>().Navigation(r => r.Court).AutoInclude();
            builder.Entity<Reservation>().Navigation(r => r.User).AutoInclude();

            // Fluent API to configure ApplicationUser and Reservation Entities for One-To-Many.
            builder.Entity<ApplicationUser>()
                .HasMany(au => au.Reservations)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Restrict);

            ////Seeding the relation between our user and role to AspNetUserRoles table
            //builder.Entity<IdentityRole>()
            //    .HasData(new IdentityRole
            //    {
            //        Id = new Guid().ToString(),
            //        Name = "Administrator",
            //        NormalizedName = "ADMINISTRATOR"
            //    });

            base.OnModelCreating(builder);
        }
    }
}
