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
        public string Type { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public string Rank { get; set; }

        [JsonProperty(PropertyName = "birthplace")]
        public string BirthPlace { get; set; }

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
            this.Type = String.IsNullOrEmpty(prtc.type.Name) ? "" : prtc.type.Name;
            this.Rank = String.IsNullOrEmpty(prtc.Rank) ? "" : prtc.Rank;
            this.BirthPlace = String.IsNullOrEmpty(prtc.BirthPlace) ? "" : prtc.BirthPlace;

            if (prtc.Birthday != null)
                this.Birthday = (DateTime)prtc.Birthday;

            if (prtc.Deathday != null)
                this.Deathday = prtc.Deathday.Value;

            Photos = new List<PhotoDto>();
            if (prtc.Photos.Any())
            {
                foreach (var item in prtc.Photos)
                {
                    Photos.Add(new PhotoDto(item));
                }
            }

            Documents = new List<DocumentsDto>();
            if (prtc.Documents != null && prtc.Documents.Any())
            {
                foreach (var item in prtc.Documents)
                {
                    Documents.Add(new DocumentsDto(item));
                }
            }

        }

    }
}

