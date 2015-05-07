using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParticipantsOfWar.Models
{
    /// <summary>
    /// Участники ВОВ   
    /// </summary>
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ParticipantId { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string ShortName { get; set; }
        public string Rank { get; set; }
        public string BirthPlace { get; set; }
         [Column(TypeName = "datetime2")]
        public DateTime? Birthday { get; set; }
         [Column(TypeName = "datetime2")]
        public DateTime? Deathday { get; set; }
        [MaxLength]
        public string Description { get; set; }

        public virtual ParticipantType type { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}