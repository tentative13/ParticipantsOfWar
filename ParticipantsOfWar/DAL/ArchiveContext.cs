namespace ParticipantsOfWar.DAL
{
    using ParticipantsOfWar.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ArchiveContext : DbContext
    {
        public ArchiveContext()
            : base("name=Archive")
        {
        }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ParticipantType> ParticipantTypes { get; set; }
        //public DbSet<DocumentType> DocumentTypes { get; set; }

    }
}