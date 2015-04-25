using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.DAL
{
    public class ArchiveInitializer : DropCreateDatabaseAlways<ArchiveContext>
    {
        protected override void Seed(ArchiveContext context)
        {
            var p = new List<Participant>
            {
            new Participant{ParticipantId=Guid.NewGuid(), Firstname="Александр",Surname="Волк", Birthday = DateTime.Parse("2005-09-01"), Middlename="Викторович"}
            };

            p.ForEach(s => context.Participants.Add(s));
            context.SaveChanges();

            var types = new List<ParticipantType>
            {
                new ParticipantType{Name = "Участники ВОВ", ParticipantTypeId=Guid.NewGuid()},
                new ParticipantType{Name = "Труженники тыла", ParticipantTypeId=Guid.NewGuid()},   
                new ParticipantType{Name = "Дети войны", ParticipantTypeId=Guid.NewGuid()},
                new ParticipantType{Name = "Участники горячих точек", ParticipantTypeId=Guid.NewGuid()},
                new ParticipantType{Name= "Репрессированные", ParticipantTypeId=Guid.NewGuid()}
            };
            types.ForEach(s => context.ParticipantTypes.Add(s));
            context.SaveChanges();
        }
    }
}