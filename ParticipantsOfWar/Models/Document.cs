using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.Models
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; }
        public byte[] DocumentBytes { get; set; }
        public string Extension { get; set; }
        
    }
}