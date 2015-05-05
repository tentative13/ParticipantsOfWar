namespace ParticipantsOfWar.Migrations
{
    using ParticipantsOfWar.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<ParticipantsOfWar.DAL.ArchiveContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ParticipantsOfWar.DAL.ArchiveContext context)
        {
            
        }
    }
}
