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

        public DocumentsDto(Document doc)
        {
            this.DocumentId = doc.DocumentId.ToString();
            this.Extension = String.IsNullOrEmpty(doc.Extension) ? "" : doc.Extension;

            if(doc.DocumentName != null)
            {
                this.name = doc.DocumentName;
            }
            else
            {
                this.name = "Имя файла отсутсвует";
            }

        }
    }
}