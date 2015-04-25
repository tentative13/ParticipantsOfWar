using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.Models
{
    /// <summary>
    /// Тип участника
    /// </summary>
    public class ParticipantType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ParticipantTypeId { get; set; }
        public int Priority { get; set; }
        public string Name { get; set; }
    }
}