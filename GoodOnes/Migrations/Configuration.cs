namespace GoodOnes.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using GoodOnes.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<GoodOnes.DAL.GOContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(GoodOnes.DAL.GOContext context)
        {
            context.Seed();
            //var events = new List<Event>
            //{
            //    new Event { Date = "2016-02-04", Title = "Dance Party", Description = "", Price = 20M, MinCapacity = 30, 
            //        MaxCapacity = 50, Line1 = "8-10pm, Cambridge MA", Line2 = "Open Bar, White or Black Attire, 30-50 people" },
            //    new Event { Date = "2016-02-08", Title = "Jim's Birthday Party", Description = "", Price = 20M, MinCapacity = 20,
            //        MaxCapacity = 40, Line1 = "6-11pm, Boston MA", Line2 = "Prix Fixe, Red and Blue Attire, 20-40 people" },
            //    new Event { Date = "2016-02-14", Title = "Valentine's Day Dinner", Description = "", Price = 20M, MinCapacity = 20,
            //        MaxCapacity = 40, Line1 = "6-11pm, Boston MA", Line2 = "Prix Fixe, Blue and Yellow Attire, 20-40 people" },
            //    new Event { Date = "2016-02-27", Title = "Sam's Birthday Party", Description = "", Price = 20M, MinCapacity = 15,
            //        MaxCapacity = 25, Line1 = "1-3pm, Newton MA", Line2 = "Prix Fixe, No Dress Code, 15-25 people" },
            //    new Event { Date = "2016-03-15", Title = "Matt's Birthday Party", Description = "", Price = 20M, MinCapacity = 10,
            //        MaxCapacity = 20, Line1 = "7-10pm, Newton MA", Line2 = "Swing Dancing, Yellow Overalls, 10-20 people" }
            //};
            //events.ForEach(e => context.Events.AddOrUpdate(x => x.Date, e));
            //context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
