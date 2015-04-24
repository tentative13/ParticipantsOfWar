using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using ParticipantsOfWar.Models;
using ParticipantsOfWar.BLL;
using ParticipantsOfWar.Dto;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        [Route("All")]
        public HttpResponseMessage GetAllParticipants()
        {
            List<ParticipantsDto> response = new List<ParticipantsDto>();
            var all_prtc = _archiveRepo.GetAll();
            foreach(var item in all_prtc)
            {
                response.Add(new ParticipantsDto(item));
            }

            return response.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, response)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("GetTypes")]
        public HttpResponseMessage GetAllTypes()
        {
            List<ParticipantTypeDto> response = new List<ParticipantTypeDto>();
            var all_prtc = _archiveRepo.GetAllTypes();
            foreach(var item in all_prtc)
            {
                response.Add(new ParticipantTypeDto(item));
            }

            return response.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, response)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // GET: api/Participants/5
        [ResponseType(typeof(Participant))]
        public IHttpActionResult GetParticipant(Guid id)
        {
            Participant participant = _archiveRepo.Get<Participant>(id);
            if (participant == null) return NotFound();
            
            return Ok(participant);
        }

        // PUT: api/Participants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipant(Guid id, Participant participant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != participant.ParticipantId) return BadRequest();

            _archiveRepo.Update<Participant>(participant);
            try
            {
                _archiveRepo.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Participants
        [HttpPost]
        [ResponseType(typeof(Participant))]
        public IHttpActionResult PostParticipant(Participant participant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _archiveRepo.Add<Participant>(participant);

            try
            {
                _archiveRepo.Commit();
            }
            catch (DbUpdateException)
            {
                if (ParticipantExists(participant.ParticipantId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = participant.ParticipantId }, participant);
        }

        //// DELETE: api/Participants/5
        //[ResponseType(typeof(Participant))]
        //public IHttpActionResult DeleteParticipant(Guid id)
        //{
        //    Participant participant = db.Participants.Find(id);
        //    if (participant == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Participants.Remove(participant);
        //    db.SaveChanges();

        //    return Ok(participant);
        //}

        private bool ParticipantExists(Guid id)
        {
            return _archiveRepo.List<Participant>(e => e.ParticipantId == id).Count > 0;
        }
    }
}