using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace lab2.Controllers
{
	public static class SessionHelper
	{
		public static int GetAuthorizedUser(object sessionCode)
		{
			var httpRequest = WebRequest.CreateHttp("http://localhost:27433/api/session/GetAuthorizedUserId?sessionCode="+sessionCode);
			httpRequest.Method = "GET";
			HttpWebResponse httpResponse;
			try
			{
				httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			}
			catch (Exception)
			{
				return 0;
			}
			if (httpResponse.StatusCode == HttpStatusCode.OK)
			{
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					return int.Parse(result);
				}
			}
			return 0;
		}

		public static int Logout(object sessionCode)
		{
			var httpRequest = WebRequest.CreateHttp("http://localhost:27433/api/session/LogoutUser?sessionCode=" + sessionCode);
			httpRequest.Method = "POST";
			HttpWebResponse httpResponse;
			httpRequest.ContentLength = 0;
			try
			{
				httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			}
			catch (Exception)
			{
				return 0;
			}
			if (httpResponse.StatusCode == HttpStatusCode.OK)
			{
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					return int.Parse(result);
				}
			}
			return 0;
		}

	}
}