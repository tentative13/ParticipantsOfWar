using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ParticipantsOfWar.Models;

namespace ParticipantsOfWar.Dto
{
    public class DocumentsDto
    {
        [JsonProperty(PropertyName = "documentId")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "extension")]
        public string Extension { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        public static DocumentsDto MapToDto (Document doc)
        {
            DocumentsDto dto = new DocumentsDto();
            dto.DocumentId = doc.DocumentId.ToString();
            dto.Extension = String.IsNullOrEmpty(doc.Extension) ? "" : doc.Extension;

            if(doc.DocumentName != null)
            {
                dto.name = doc.DocumentName;
            }
            else
            {
                dto.name = "Имя файла отсутсвует";
            }

            return dto;
        }
    }
}