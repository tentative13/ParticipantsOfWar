﻿using System;
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

        [HttpGet]
        [AllowAnonymous]
        [Route("GetTypes")]
        [ResponseType(typeof(ParticipantTypeDto[]))]
        public HttpResponseMessage GetAllTypes()
        {
            List<ParticipantTypeDto> response = new List<ParticipantTypeDto>();
            var all_prtc = _archiveRepo.GetAllTypes();
            foreach(var item in all_prtc)
            {
                response.Add(ParticipantTypeDto.MapToDto(item));
            }

            return response.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, response.ToArray())
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // GET: api/Participants/5
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(ParticipantsDto))]
        public IHttpActionResult GetParticipant(Guid id)
        {
            Participant participant = _archiveRepo.Get<Participant>(id);
            if (participant == null) return NotFound();
            return Ok(ParticipantsDto.MapToDto(participant));
        }

        // PUT: api/Participants/5
        [HttpPut]
        [Authorize]
        public IHttpActionResult PutParticipant(Guid id, ParticipantsDto participant)
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
            dbEntity.Rank = participant.Rank == null ? "" : participant.Rank;
            dbEntity.BirthPlace = participant.BirthPlace == null ? "" : participant.BirthPlace;

            dbEntity.Birthday = participant.Birthday;
            dbEntity.Deathday = participant.Deathday;

            if (participant.Birthday == DateTime.MinValue) dbEntity.Birthday = null;
            if (participant.Deathday == DateTime.MinValue) dbEntity.Deathday = null;

            var newtype = _archiveRepo.List<ParticipantType>(x => x.Name == participant.Type.Name).FirstOrDefault();
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
        [Authorize]
        [ResponseType(typeof(ParticipantsDto))]
        public IHttpActionResult PostParticipant(ParticipantsDto participant)
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
            newone.Rank = participant.Rank == null ? "" : participant.Rank;
            newone.BirthPlace = participant.BirthPlace == null ? "" : participant.BirthPlace;
            if (participant.Birthday == DateTime.MinValue) newone.Birthday = null;
            if (participant.Deathday == DateTime.MinValue) newone.Deathday = null;

            if (participant.Type != null)
            {
                var newtype = _archiveRepo.List<ParticipantType>(x => x.Name == participant.Type.Name).FirstOrDefault();
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

            ParticipantsDto response = ParticipantsDto.MapToDto(newone);

            return CreatedAtRoute("DefaultApi", new { id = response.guid }, response);
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteParticipant(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id == null) return BadRequest();

            try
            {
                Participant participant = _archiveRepo.Get<Participant>(id);
                if (participant == null) return NotFound();

                if(participant.Documents.Any())
                {
                    var docs_ids = participant.Documents.Select(x => x.DocumentId).ToList();
                    for (int i=0; i < docs_ids.Count; i++ )
                    {
                        _archiveRepo.Delete<Document>(docs_ids[i]);
                    }
                }

                if (participant.Photos.Any())
                {
                    var photo_ids = participant.Photos.Select(x => x.PhotoId).ToList();
                    for (int i = 0; i < photo_ids.Count; i++)
                    {
                        _archiveRepo.Delete<Photo>(photo_ids[i]);
                    }
                }
                _archiveRepo.Delete<Participant>(id);
                _archiveRepo.Commit();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        private bool ParticipantExists(Guid id)
        {
            return _archiveRepo.List<Participant>(e => e.ParticipantId == id).Count > 0;
        }
    }
}