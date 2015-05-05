using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.Models
{
    /// <summary>
    /// Документы участника
    /// </summary>
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DocumentId { get; set; }
        public string DocumentName { get; set; }
        [MaxLength]
        public byte[] DocumentBytes { get; set; }
        public string Extension { get; set; }

        //public virtual DocumentType type { get; set; }

    }
}