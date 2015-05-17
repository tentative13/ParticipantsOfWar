using Microsoft.AspNet.Identity.EntityFramework;
using ParticipantsOfWar.Models;
using System.Data.Entity;


namespace ParticipantsOfWar.DAL
{
    public class ArchiveContext : IdentityDbContext<ApplicationUser> //DbContext
    {
        public ArchiveContext()
            : base("name=ArchiveLocal")
        {

        }

        public static ArchiveContext Create()
        {
            return new ArchiveContext();
        }
               
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ParticipantType> ParticipantTypes { get; set; }
        //public DbSet<DocumentType> DocumentTypes { get; set; }
    }
}