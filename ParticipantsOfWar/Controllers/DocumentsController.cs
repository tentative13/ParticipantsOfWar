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

        [Route("GetPhoto/{id:guid}")]
        public HttpResponseMessage GetPhoto(Guid id)
        {
            var photo = _archiveRepo.Get<Photo>(id);

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

        [Route("GetDocument/{id:guid}")]
        public HttpResponseMessage GetDocument(Guid id)
        {
            var doc = _archiveRepo.Get<Document>(id);

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                response.Content = new StreamContent(new MemoryStream(doc.DocumentBytes));
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = doc.type.Name+doc.Extension;

            }
            catch (FileNotFoundException)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }

            return response;
        }
    }
}