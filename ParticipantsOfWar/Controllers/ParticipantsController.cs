using ParticipantsOfWar.BLL;
using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParticipantsOfWar.Controllers
{
    [RoutePrefix("api/Participants")]
    public class ParticipantsController : ApiController
    {
        public IArchiveRepository archiveRepo;
        public ParticipantsController(IArchiveRepository _archiveRepo)
        {
            archiveRepo = _archiveRepo;
        }
        public IEnumerable<Participant> GetAllParticipants()
        {
            return archiveRepo.GetAll();
        }
    }
}