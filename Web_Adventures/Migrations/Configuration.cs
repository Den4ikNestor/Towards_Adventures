using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Adventures.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Web_Adventures.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Web_Adventures.Models.ApplicationDbContext";
        }

        protected override void Seed(Web_Adventures.Models.ApplicationDbContext context)
        {
            //._.//
        }
    }
}