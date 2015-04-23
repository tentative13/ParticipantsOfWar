using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.Models
{
    public class Photo
    {
        [Key]
        public Guid PhotoId { get; set; }
        public byte[] PhotoBytes { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
    }
}