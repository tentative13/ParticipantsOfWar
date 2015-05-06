using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ParticipantsOfWar.Models;

namespace ParticipantsOfWar.Dto
{
    public class ParticipantsDto
    {
        [JsonProperty(PropertyName = "guid")]
        public string guid { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string Firstname { get; set; }

        [JsonProperty(PropertyName = "middlename")]
        public string Middlename { get; set; }

        [JsonProperty(PropertyName = "shortName")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "birthday")]
        public DateTime Birthday { get; set; }
        
        [JsonProperty(PropertyName = "deathday")]
        public DateTime Deathday { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "type")]
        public ParticipantsTypeObjDto Type { get; set; }

        [JsonProperty(PropertyName = "photos")]
        public List<PhotoDto> Photos { get; set; }

        [JsonProperty(PropertyName = "documents")]
        public List<DocumentsDto> Documents { get; set; }

        public ParticipantsDto(Participant prtc)
        {
            this.guid = prtc.ParticipantId.ToString();
            this.Surname = String.IsNullOrEmpty(prtc.Surname) ? "" : prtc.Surname;
            this.Firstname = String.IsNullOrEmpty(prtc.Firstname) ? "" : prtc.Firstname;
            this.Middlename = String.IsNullOrEmpty(prtc.Middlename) ? "" : prtc.Middlename;
            this.ShortName = String.IsNullOrEmpty(prtc.ShortName) ? "" : prtc.ShortName;
            this.Description = String.IsNullOrEmpty(prtc.Description) ? "" : prtc.Description;
            

            if(prtc.type != null)
            {
                ParticipantsTypeObjDto typeobj = new ParticipantsTypeObjDto();
                typeobj.name = prtc.type.Name;
                typeobj.value = prtc.type.Priority;
                this.Type = typeobj;
            }
           

            if (prtc.Birthday != null)
                this.Birthday = (DateTime)prtc.Birthday;

            if (prtc.Deathday != null)
                this.Deathday = prtc.Deathday.Value;

            if (prtc.Photos != null)
            {
                if (prtc.Photos.Any())
                {
                    this.Photos = new List<PhotoDto>();

                    foreach (var item in prtc.Photos)
                    {
                        Photos.Add(new PhotoDto(item));
                    }
                }
            }

            if (prtc.Documents != null)
            {
                if (prtc.Documents.Any())
                {
                    this.Documents = new List<DocumentsDto>();

                    foreach (var item in prtc.Documents)
                    {
                        Documents.Add(new DocumentsDto(item));
                    }
                }
            }
            
        }

    }

    public class ParticipantsTypeObjDto
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "value")]
        public int value { get; set; }
    }

    public class ParticipantsInDto
    {
        [JsonProperty(PropertyName = "guid")]
        public string guid { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string Firstname { get; set; }

        [JsonProperty(PropertyName = "middlename")]
        public string Middlename { get; set; }

        [JsonProperty(PropertyName = "shortName")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty(PropertyName = "deathday")]
        public DateTime Deathday { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "type")]
        public ParticipantsTypeObjDto Type { get; set; }

        [JsonProperty(PropertyName = "photos")]
        public PhotoInDto[] Photos { get; set; }

        [JsonProperty(PropertyName = "documents")]
        public DocumentsInDto[] Documents { get; set; }
    }
    public class PhotoInDto
    {
        [JsonProperty(PropertyName = "photoId")]
        public string PhotoId { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }

    public class DocumentsInDto
    {
        [JsonProperty(PropertyName = "documentId")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }
    }
}

