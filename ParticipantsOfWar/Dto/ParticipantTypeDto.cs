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


		public ParticipantTypeDto (ParticipantType type)
		{
			this.guid = type.ParticipantTypeId.ToString();
			this.Name = String.IsNullOrEmpty(type.Name) ? "" : type.Name;
		}
	}
}