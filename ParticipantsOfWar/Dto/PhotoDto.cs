using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ParticipantsOfWar.Models;

namespace ParticipantsOfWar.Dto
{
    public class PhotoDto
    {
        [JsonProperty(PropertyName = "photoId")]
        public string PhotoId { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        public PhotoDto(Photo item)
        {
            this.PhotoId = item.PhotoId.ToString();
            this.Description = String.IsNullOrEmpty(item.Description) ? "" : item.Description;

        }
    }
}