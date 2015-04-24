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
        public string Birthday { get; set; }
        
        [JsonProperty(PropertyName = "deathday")]
        public string Deathday { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }



        public ParticipantsDto(Participant prtc)
        {
            this.guid = prtc.ParticipantId.ToString();
            this.Surname = String.IsNullOrEmpty(prtc.Surname) ? "" : prtc.Surname;
            this.Firstname = String.IsNullOrEmpty(prtc.Firstname) ? "" : prtc.Firstname;
            this.Middlename = String.IsNullOrEmpty(prtc.Middlename) ? "" : prtc.Middlename;
            this.ShortName = String.IsNullOrEmpty(prtc.ShortName) ? "" : prtc.ShortName;
            this.Birthday = prtc.Birthday == null ? "" : prtc.Birthday.ToString("dd.MM.yyyy");
            this.Deathday = prtc.Deathday == null ? "" : prtc.Deathday.Value.ToString("dd.MM.yyyy");
            this.Description = String.IsNullOrEmpty(prtc.Description) ? "" : prtc.Description;
        }

    }
}

