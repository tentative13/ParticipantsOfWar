using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.DAL
{
    public class ArchiveInitializer : DropCreateDatabaseIfModelChanges<ArchiveContext>
    {
        protected override void Seed(ArchiveContext context)
        {
            var p = new List<Participant>
            {
            new Participant{Firstname="Александр",Surname="Волк", Birthday = DateTime.Parse("2005-09-01"), Middlename="Викторович"}
            };

            p.ForEach(s => context.Participants.Add(s));
            context.SaveChanges();
        }
    }
}