using System;
using System.Collections.Generic;
using System.Text;

namespace EarwigPOC.Entities
{
	class SessionEntity
	{
		public string SessionName { get; set; }
		public IList<PlayerEntity> PlayerList { get; set; }
	}
}
