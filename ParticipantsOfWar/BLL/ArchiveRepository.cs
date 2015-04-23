using ParticipantsOfWar.DAL;
using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.BLL
{
    public class ArchiveRepository : IArchiveRepository, IDisposable
    {
        private ArchiveContext db = new ArchiveContext();

        public IEnumerable<Participant> GetAll()
        {
            return db.Participants;
        }

        public IEnumerable<ParticipantType> GetAllTypes()
        {
            return db.ParticipantTypes;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}