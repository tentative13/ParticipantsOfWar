using ParticipantsOfWar.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ParticipantsOfWar.DAL
{
    public class ArchiveContext: DbContext
    {
        public ArchiveContext()
            : base("ArchiveContext")
        {
            Database.SetInitializer<ArchiveContext>(new CreateDatabaseIfNotExists<ArchiveContext>());
        }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ParticipantType> ParticipantTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}