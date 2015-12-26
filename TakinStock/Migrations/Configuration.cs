namespace TakinStock.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TakinStock.Models.StockContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TakinStock.Models.StockContext context)
        {
            var users = new List<Users>
            {
                new Users { Email = "user1@seed.com" },
                new Users { Email = "user2@seed.com" }
            };
            users.ForEach(u => context.User.AddOrUpdate(u));
            context.SaveChanges();

            var query = from u in context.User where u.Email.Contains("user1@seed.com") select u;
            Users user1 = query.SingleOrDefault();

            var query2 = from u in context.User where u.Email.Contains("user2@seed.com") select u;
            Users user2 = query2.SingleOrDefault();

            DateTime purchase = DateTime.Now.Date;

            var items = new List<Items>
            {
                new Items {Type = "Electronics", Make = "Samsung", Description = "\"48\" HDTV", Owner = user1, PurchaseDate = purchase},
                new Items {Type = "Instruments", Make = "Fender", Description = "Stratocaster", Owner = user2, PurchaseDate = purchase },
                new Items {Type = "Computers/Phones", Make = "Apple", Description = "iPhone 6", Owner = user2, PurchaseDate = purchase },
                new Items {Type = "Electronics", Make = "Onkyo", Description = "Surround Sound Receiver", Owner = user1, PurchaseDate = purchase },
                new Items {Type = "Instruments", Make = "Gibson", Description = "Les Paul", Owner = user2, PurchaseDate = purchase },
                new Items {Type = "Computers/Phones", Make = "Apple", Description = "\"15\" MacBook Pro", Owner = user1, PurchaseDate = purchase }
            };
            items.ForEach(i => context.Items.AddOrUpdate(i));
            context.SaveChanges();



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
