using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.BLL
{
    public interface IArchiveRepository
    {
        IEnumerable<Participant> GetAll();
    }   
}