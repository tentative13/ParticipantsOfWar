using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.Models
{
    /// <summary>
    /// тип документа, напр. военный билет
    /// </summary>
    public class DocumentType
    {
        [Key]
        public Guid DocumentTypeId { get; set; }
        public string Name { get; set; }
    }
}