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
        [ResponseType(typeof(ParticipantsDto))]
        public IHttpActionResult GetParticipant(Guid id)
        {
            Participant participant = _archiveRepo.Get<Participant>(id);
            if (participant == null) return NotFound();

            ParticipantsDto response = new ParticipantsDto(participant);
            return Ok(response);
        }

        // PUT: api/Participants/5
        public IHttpActionResult PutParticipant(Guid id, ParticipantsInDto participant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != new Guid(participant.guid)) return BadRequest();

            var dbEntity = _archiveRepo.Get<Participant>(id);
            if (dbEntity == null) return NotFound();


            dbEntity.Firstname = participant.Firstname == null ? "" : participant.Firstname;
            dbEntity.Middlename = participant.Middlename == null ? "" : participant.Middlename;
            dbEntity.Surname = participant.Surname == null ? "" : participant.Surname; 
            dbEntity.Description = participant.Description == null ? "" : participant.Description;
            dbEntity.ShortName = participant.ShortName == null ? "" : participant.ShortName;

            dbEntity.Birthday = participant.Birthday;
            dbEntity.Deathday = participant.Deathday;


            var newtype = _archiveRepo.List<ParticipantType>(x => x.Name == participant.Type.name).FirstOrDefault();
            if (newtype != null)
            {
                dbEntity.type = newtype;
            }

            _archiveRepo.Update<Participant>(dbEntity);

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
        public IHttpActionResult PostParticipant(ParticipantsInDto participant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Participant newone = new Participant();

            newone.Birthday = participant.Birthday;
            newone.Deathday = participant.Deathday;
            newone.Firstname = participant.Firstname == null ? "" : participant.Firstname;
            newone.Middlename = participant.Middlename == null ? "" : participant.Middlename;
            newone.Surname = participant.Surname == null ? "" : participant.Surname;
            newone.Description = participant.Description == null ? "" : participant.Description;
            newone.ShortName = participant.ShortName == null ? "" : participant.ShortName;
            newone.BirthPlace = "";
            newone.Rank = "";


            if (participant.Type != null)
            {
                var newtype = _archiveRepo.List<ParticipantType>(x => x.Name == participant.Type.name).FirstOrDefault();
                if (newtype != null)
                {
                    newone.type = newtype;
                }
            }

            _archiveRepo.Add<Participant>(newone);

            try
            {
                _archiveRepo.Commit();
            }
            catch (DbUpdateException ex)
            {
                if (ParticipantExists(new Guid(participant.guid)))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            ParticipantsDto response = new ParticipantsDto(newone);

            return CreatedAtRoute("DefaultApi", new { id = response.guid }, response);
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