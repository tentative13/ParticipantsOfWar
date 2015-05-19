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
using System.IO;
using System.Web;
using System.Collections.Specialized;

namespace ParticipantsOfWar.Controllers
{
    [RoutePrefix("api/Documents")]
    public class DocumentsController : ApiController
    {
        public IArchiveRepository _archiveRepo;

        public DocumentsController(IArchiveRepository archiveRepo)
        {
            _archiveRepo = archiveRepo;
        }

        [AllowAnonymous]
        [Route("GetPhoto/{id:guid}")]
        public HttpResponseMessage GetPhoto(Guid id)
        {
            var photo = _archiveRepo.Get<Photo>(id);
            if (photo == null) return new HttpResponseMessage(HttpStatusCode.NotFound);

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                response.Content = new StreamContent(new MemoryStream(photo.PhotoBytes));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
               // response.Content.Headers.ContentDisposition.FileName = photo.Description + photo.Extension;
            }
            catch (FileNotFoundException)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }

            return response;
        }

        [AllowAnonymous]
        [Route("GetDocument/{id:guid}")]
        public HttpResponseMessage GetDocument(Guid id)
        {
            var doc = _archiveRepo.Get<Document>(id);
            if (doc == null) return new HttpResponseMessage(HttpStatusCode.NotFound);

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                response.Content = new StreamContent(new MemoryStream(doc.DocumentBytes));
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = doc.DocumentName + doc.Extension;
            }
            catch (FileNotFoundException)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }

            return response;
        }

        [HttpPost]
        [Authorize]
        [ResponseType(typeof(PhotoDto[]))]
        [Route("UploadPhoto")]
        public IHttpActionResult UploadPhoto()
        {
            List<PhotoDto> response = new List<PhotoDto>();

            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;
                var ParticipantGuid = HttpContext.Current.Request.Form["ParticipantGuid"].ToString();
                
                if(String.IsNullOrWhiteSpace(ParticipantGuid)) return NotFound();

                var guid = new Guid(ParticipantGuid);
                if (guid == null) return NotFound();

                Participant participant = _archiveRepo.Get<Participant>(guid);
                if (participant == null) return NotFound();
               

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    int numBytes = file.ContentLength;
                    BinaryReader br = new BinaryReader(file.InputStream);
                    byte[] data = br.ReadBytes(numBytes);

                    Photo newphoto = new Photo();
                    newphoto.PhotoBytes = data;
                    newphoto.Extension = '.' +  file.FileName.Split('.')[1];
                    newphoto.Description = "Фотография";

                    _archiveRepo.Add<Photo>(newphoto);
                    participant.Photos.Add(newphoto);
                    _archiveRepo.Update<Participant>(participant);

                    try
                    {
                        _archiveRepo.Commit();
                        response.Add(PhotoDto.MapToDto(newphoto));
                    }
                    catch (DbUpdateException)
                    {
                        return NotFound();
                    }
                }
               
            }
            return Ok(response.ToArray());
        }

        [HttpPost]
        [Authorize]
        [ResponseType(typeof(DocumentsDto[]))]
        [Route("UploadDocument")]
        public IHttpActionResult UploadDocument()
        {
            List<DocumentsDto> response = new List<DocumentsDto>();

            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;
                var ParticipantGuid = HttpContext.Current.Request.Form["ParticipantGuid"].ToString();

                if (String.IsNullOrWhiteSpace(ParticipantGuid)) return NotFound();

                var guid = new Guid(ParticipantGuid);
                if (guid == null) return NotFound();

                Participant participant = _archiveRepo.Get<Participant>(guid);
                if (participant == null) return NotFound();



                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    int numBytes = file.ContentLength;
                    BinaryReader br = new BinaryReader(file.InputStream);
                    byte[] data = br.ReadBytes(numBytes);

                    Document newdoc = new Document();

                    newdoc.DocumentBytes = data;
                    newdoc.DocumentName = file.FileName;
                    newdoc.Extension = '.' + file.FileName.Split('.')[1];

                    _archiveRepo.Add<Document>(newdoc);
                    participant.Documents.Add(newdoc);
                    _archiveRepo.Update<Participant>(participant);
                    try
                    {
                        _archiveRepo.Commit();
                        response.Add(DocumentsDto.MapToDto(newdoc));
                    }
                    catch (DbUpdateException)
                    {
                        return NotFound();
                    }
                }
            }
            return Ok(response.ToArray());
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteDocument/{id:guid}")]
        public IHttpActionResult DeleteDocument(Guid id)
        {
            var doc = _archiveRepo.Get<Document>(id);
            if (doc == null) return NotFound();
            _archiveRepo.Delete<Document>(doc);
            try
            {
                _archiveRepo.Commit();
            }
            catch (DbUpdateException)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("DeletePhoto/{id:guid}")]
        public IHttpActionResult DeletePhoto(Guid id)
        {
            var photo = _archiveRepo.Get<Photo>(id);
            if (photo == null) return NotFound();
            _archiveRepo.Delete<Photo>(photo);
            try
            {
                _archiveRepo.Commit();
            }
            catch (DbUpdateException)
            {
                return NotFound();
            }

            return Ok();
        }
    }

}