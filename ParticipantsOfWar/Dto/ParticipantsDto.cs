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
        public ParticipantTypeDto Type { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public string Rank { get; set; }

        [JsonProperty(PropertyName = "birthplace")]
        public string BirthPlace { get; set; }

        [JsonProperty(PropertyName = "photos")]
        public PhotoDto[] Photos { get; set; }

        [JsonProperty(PropertyName = "documents")]
        public DocumentsDto[] Documents { get; set; }

        public static ParticipantsDto MapToDto (Participant prtc)
        {
            ParticipantsDto dto = new ParticipantsDto();
            dto.guid = prtc.ParticipantId.ToString();
            dto.Surname = String.IsNullOrEmpty(prtc.Surname) ? "" : prtc.Surname;
            dto.Firstname = String.IsNullOrEmpty(prtc.Firstname) ? "" : prtc.Firstname;
            dto.Middlename = String.IsNullOrEmpty(prtc.Middlename) ? "" : prtc.Middlename;
            dto.ShortName = String.IsNullOrEmpty(prtc.ShortName) ? "" : prtc.ShortName;
            dto.Description = String.IsNullOrEmpty(prtc.Description) ? "" : prtc.Description;
            dto.Rank = String.IsNullOrEmpty(prtc.Rank) ? "" : prtc.Rank;
            dto.BirthPlace = String.IsNullOrEmpty(prtc.BirthPlace) ? "" : prtc.BirthPlace;

            if(prtc.type != null)
            {
                dto.Type = ParticipantTypeDto.MapToDto(prtc.type);
            }
           
            if (prtc.Birthday != null)
                dto.Birthday = (DateTime)prtc.Birthday;

            if (prtc.Deathday != null)
                dto.Deathday = (DateTime)prtc.Deathday.Value;

            if (prtc.Photos != null)
            {
                if (prtc.Photos.Any())
                {
                    List<PhotoDto> photos = new List<PhotoDto>();

                    foreach (var item in prtc.Photos)
                    {
                        photos.Add(PhotoDto.MapToDto(item));
                    }

                    dto.Photos = photos.ToArray();
                }
            }

            if (prtc.Documents != null)
            {
                if (prtc.Documents.Any())
                {
                    List<DocumentsDto> docs = new List<DocumentsDto>();

                    foreach (var item in prtc.Documents)
                    {
                        docs.Add(DocumentsDto.MapToDto(item));
                    }

                    dto.Documents = docs.ToArray();
                }
            }

            return dto;
        }

    }
 }

