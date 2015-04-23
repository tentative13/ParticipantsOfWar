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
        public IArchiveRepository _archiveRepo;
        public ParticipantsController(IArchiveRepository archiveRepo)
        {
            _archiveRepo = archiveRepo;
        }
        public IEnumerable<Participant> GetAllParticipants()
        {
            return _archiveRepo.GetAll();
        }
        [Route("GetTypes")]
        public IEnumerable<ParticipantType> GetAllTypes()
        {
            return _archiveRepo.GetAllTypes();
        }
    }
}