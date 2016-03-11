using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using GoodOnes.Entities;

namespace GoodOnes.DAL
{
    public class GOContext : DbContext
    {
        public GOContext() : base("GOContext") { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Person>()
                .Map<Guest>(m => m.Requires("PersonType").HasValue("Guest"))
                .Map<Member>(m => m.Requires("PersonType").HasValue("Member"));
        }

        public void Seed()
        {
            var events = new List<Event> {
                new Event { Date = "2016-02-08", Title = "Jim's Birthday Party", Description = "", Price = 20M, MinCapacity = 20,
                    MaxCapacity = 40, Line1 = "6-11pm, Boston MA", Line2 = "Prix Fixe, Red and Blue Attire, 20-40 people" },
                new Event { Date = "2016-02-14", Title = "Valentine's Day Dinner", Description = "", Price = 20M, MinCapacity = 20,
                    MaxCapacity = 40, Line1 = "6-11pm, Boston MA", Line2 = "Prix Fixe, Blue and Yellow Attire, 20-40 people" },
                new Event { Date = "2016-02-27", Title = "Sam's Birthday Party", Description = "", Price = 20M, MinCapacity = 15,
                    MaxCapacity = 25, Line1 = "1-3pm, Newton MA", Line2 = "Prix Fixe, No Dress Code, 15-25 people" },
                new Event { Date = "2016-03-02", Title = "Dance Party", Description = "", Price = 20M, MinCapacity = 30, 
                    MaxCapacity = 50, Line1 = "8-10pm, Cambridge MA", Line2 = "Open Bar, White or Black Attire, 30-50 people" },
                new Event { Date = "2016-03-15", Title = "Matt's Birthday Party", Description = "", Price = 20M, MinCapacity = 10,
                    MaxCapacity = 20, Line1 = "7-10pm, Newton MA", Line2 = "Swing Dancing, Yellow Overalls, 10-20 people" }
            };
            events.ForEach(e => Events.AddOrUpdate(x => x.Date, e));
            SaveChanges();

            var questions = new List<Question> {
                new Question { ControlType = "choice", Text = "Would you rather ... ?", Live = true, Sequence = 1,
                    PossibleAnswers = "Chill with Eminem|Chill with Jay-Z", CanBeBlank = false },
                new Question { ControlType = "choice", Text = "Which color do you find more pleasing?", Live = true, Sequence = 2,
                    PossibleAnswers = "Red|Blue|Green|Yellow", CanBeBlank = false },
                new Question { ControlType = "choice", Text = "Which animal describes you better?", Live = true, Sequence = 3,
                    PossibleAnswers = "Elephant|Frog|Parakeet", CanBeBlank = false },
                new Question { ControlType = "choice", Text = "Which candy do you like more?", Live = true, Sequence = 4,
                    PossibleAnswers = "M&M's|Sour Patch Kids", CanBeBlank = false }
            };
            questions.ForEach(q => Questions.AddOrUpdate(x => x.Text, q));
            SaveChanges();
        }
    }
}