using EarwigPOC.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EarwigPOC
{
	class GameSessionHandler
	{
		private SessionEntity sessionList;
		public GameSessionHandler()
		{
			sessionList = new SessionEntity();
			sessionList.PlayerList = new List<PlayerEntity>();
			sessionList.SessionName = "ABCD";
		}

		public string AddNewPlayer(string name)
		{
			PlayerEntity player = new PlayerEntity() { PlayerName = name };
			sessionList.PlayerList.Add(player);

			return sessionList.SessionName;
		}

		public string AddPlayersSelection(List<string> selections)
		{

			return "";
		}

	}
}
