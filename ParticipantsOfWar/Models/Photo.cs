using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhotoId { get; set; }
        [MaxLength]
        public byte[] PhotoBytes { get; set; }
        /// <summary>
        /// png, jpg и тд
        /// </summary>
        public string Extension { get; set; }
        public string Description { get; set; }
    }
}