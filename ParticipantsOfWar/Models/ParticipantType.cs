using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.Models
{
    /// <summary>
    /// Тип участника
    /// </summary>
    public class ParticipantType
    {
        [Key]
        public Guid ParticipantTypeId { get; set; }
        public string Name { get; set; }
    }
}