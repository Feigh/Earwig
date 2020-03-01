using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EarwigPOC
{
	public static class GameListeners
	{
		public static void InitializeGame(string[] prefixes)
		{
			GameSessionHandler handler = new GameSessionHandler();

			PlayerListener plistener = new PlayerListener(handler, new string[] { "http://localhost:4333/player/" });
			Thread thr = new Thread(new ThreadStart( plistener.StartListening ) ); //(new string[] { "http://localhost:4333/player/" })
			thr.Start();

			GameDataRequester glistener = new GameDataRequester(handler, new string[] { "http://localhost:4333/game/", "http://localhost:4333/select/" });
			glistener.StartListening();
		}
		//public static void StartPlayerListener(string[] prefixes)
		//{
		//	PlayerListener listener = new PlayerListener();
		//	listener.StartListening(prefixes);
		//}

		//public static void StartGameDataRequestListener(string[] prefixes)
		//{
		//	GameDataRequester listener = new GameDataRequester();
		//	listener.StartListening(prefixes);
		//}
	}
}
