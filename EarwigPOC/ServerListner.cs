using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace EarwigPOC
{
	class ServerListner
	{
		public static void StartListner(string[] prefixes)
		{
			//KeepListening(prefixes);
		}

		private void KeepListening(string[] prefixes)
		{
			if (!HttpListener.IsSupported) // kolla att 
			{
				Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
				return;
			}

			if (prefixes == null || prefixes.Length == 0)// kollar att du fick med nått
				throw new ArgumentException("prefixes");

			// Create a listener.
			HttpListener listener = new HttpListener();

			foreach (string s in prefixes)
			{
				listener.Prefixes.Add(s); // lägg till alla adresser i listnern
			}
			listener.Start(); // starta listnern
			Console.WriteLine("Listening...");

			while (true)
			{
				RepeatListening(listener);
			}

			listener.Stop();
			Console.ReadKey();
		}

		private void RepeatListening(HttpListener listner)
		{
			HttpListenerContext context = listner.GetContext(); // sitter här och väntar på en request. först när en request kommer in så går funktionen vidare.

			HttpListenerRequest request = context.Request; // hämta request datan från klienten

			Stream body = request.InputStream;
			Encoding encoding = request.ContentEncoding;
			StreamReader reader = new System.IO.StreamReader(body, encoding);
			if (request.ContentType != null)
			{
				Console.WriteLine("Client data content type {0}", request.ContentType);
			}
			Console.WriteLine("Client data content length {0}", request.ContentLength64);

			Console.WriteLine("Start of client data:");
			// Convert the data to a string and display it on the console.
			string text = reader.ReadToEnd();

			HttpListenerResponse response = context.Response; // hämta responsen aka. objektet som klienten väntar att få tillbaka

			response.AddHeader("Access-Control-Allow-Origin", "*");
			response.AddHeader("Access-Control-Allow-Headers", "*");
			response.AddHeader("Access-Control-Allow-Methods", "POST, GET, OPTIONS");

			string responseString = "<HTML><BODY> Hello world!</BODY></HTML>"; // skapa en respons sträng och lägg den i en buffer
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

			response.ContentLength64 = buffer.Length; // måste ha längden aka slutet på buffern
			System.IO.Stream output = response.OutputStream; // hämta outputstream eller kanalen som är kopplad till klienten som den väntar på ett svar från.
			output.Write(buffer, 0, buffer.Length);
			Console.WriteLine(output);
			// stäng av allt
			output.Close();
		}
	}
}
