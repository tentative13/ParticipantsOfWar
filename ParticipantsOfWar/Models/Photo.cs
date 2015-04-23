using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.Models
{
    /// <summary>
    /// фотографии участников
    /// </summary>
    public class Photo
    {
        [Key]
        public Guid PhotoId { get; set; }
        public byte[] PhotoBytes { get; set; }
        /// <summary>
        /// png, jpg и тд
        /// </summary>
        public string Extension { get; set; }
        public string Description { get; set; }
    }
}