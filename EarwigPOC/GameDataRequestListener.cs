using EarwigPOC.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace EarwigPOC
{
	class GameDataRequester
	{
		private GameSessionHandler sessionHandler;
		private string[] _prefixes;
		public GameDataRequester(GameSessionHandler handler, string[] prefixes)
		{
			sessionHandler = handler;
			_prefixes = prefixes;
		}

		public void StartListening()
		{
			if (!HttpListener.IsSupported) // kolla att 
			{
				Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
				return;
			}

			if (_prefixes == null || _prefixes.Length == 0)// kollar att du fick med nått
				throw new ArgumentException("prefixes");

			// Create a listener.
			HttpListener listener = new HttpListener();

			foreach (string s in _prefixes)
			{
				listener.Prefixes.Add(s); // lägg till alla adresser i listnern 
			}
			
			listener.Start(); // starta listnern
			Console.WriteLine("Listening...");

			while (true)
			{
				Listen(listener);
			}

			listener.Stop();
			Console.ReadKey();
		}

		private void Listen(HttpListener listner)
		{
			HttpListenerContext context = listner.GetContext(); // sitter här och väntar på en request. först när en request kommer in så går funktionen vidare.

			HttpListenerRequest request = context.Request; // hämta request datan från klienten
			string url = request.RawUrl;
			url = url.Substring(1);
			if (url == "favicon.ico")
			{
				return;
			}

			Stream body = request.InputStream;
			Encoding encoding = request.ContentEncoding;
			StreamReader reader = new System.IO.StreamReader(body, encoding);
			if (request.ContentType != null)
			{
				Console.WriteLine("Client data content type {0}", request.ContentType);
			}
			Console.WriteLine("Client data content length {0}", request.ContentLength64);

			Console.WriteLine("Start of client data:");

			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(ProcessPlayerSelection(reader.ReadToEnd()));

			HttpListenerResponse response = context.Response; // hämta responsen aka. objektet som klienten väntar att få tillbaka
			response.AddHeader("Access-Control-Allow-Origin", "*");
			response.AddHeader("Access-Control-Allow-Headers", "*");
			response.AddHeader("Access-Control-Allow-Methods", "*");
			response.ContentType = "text/plain";
			response.ContentLength64 = buffer.Length; // måste ha längden aka slutet på buffern
			System.IO.Stream output = response.OutputStream; // hämta outputstream eller kanalen som är kopplad till klienten som den väntar på ett svar från.
			output.Write(buffer, 0, buffer.Length);
			Console.WriteLine(output);
			// stäng av allt
			output.Close();
		}

		private string ProcessPlayerSelection(string resqueststring)
		{
			List<GameSelections> input = new List<GameSelections>();
			input.Add(new GameSelections() { ID = 0, Selection = "val 1" });
			input.Add(new GameSelections() { ID = 1, Selection = "val 2" });
			return JsonConvert.SerializeObject(input);
		}

	}
}
