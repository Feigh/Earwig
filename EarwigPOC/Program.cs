using System;

namespace EarwigPOC
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			//ServerListner.StartListner(new string[] { "http://localhost:4333/data/" }); // starta listnern och skicka in vilken adress, port och pathen
			//UserInputListener.StartListener(new string[] { "http://localhost:4333/input/" });
			//GameListeners.StartGameDataRequestListener.StartListener(new string[] { "http://localhost:4333/game/", "http://localhost:4333/player/" });
			GameListeners.InitializeGame(new string[] { "http://localhost:4333/player/" });
			/* när klienten startar så får de först upp en
			 * 
			 * 
			 */

		}
	}
}
