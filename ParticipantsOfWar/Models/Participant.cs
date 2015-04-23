using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ParticipantsOfWar.Models
{
    /// <summary>
    /// Участники ВОВ   
    /// </summary>
    public class Participant
    {
        [Key]
        public Guid ParticipantId { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string ShortName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public string Description { get; set; }

        public virtual ParticipantType type { get; set; }

        //public virtual ICollection<Photo> Photos {get;set;}
        //public virtual ICollection<Document> Documents { get; set; }
    }
}