using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ParticipantsOfWar.Models;

namespace ParticipantsOfWar.Dto
{
	public class ParticipantTypeDto
	{
		[JsonProperty(PropertyName = "guid")]
		public string guid { get; set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "value")]
		public int Priority { get; set; }

		public static ParticipantTypeDto MapToDto (ParticipantType type)
		{
			ParticipantTypeDto dto = new ParticipantTypeDto();
			dto.guid = type.ParticipantTypeId.ToString();
			dto.Name = String.IsNullOrEmpty(type.Name) ? "" : type.Name;
			dto.Priority = type.Priority;
			return dto;
		}
	}
}