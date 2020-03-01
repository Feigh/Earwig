using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EarwigPOC
{
	interface IBaseListener
	{
		public void StartListening();

		public void ProcessRequest(HttpListener listner);
	}
}
