using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using lab2.Models;
using lab2.ViewModels;

namespace lab2.Controllers
{
	public class UsersController : Controller
	{
		private MyShopDbContext db = new MyShopDbContext();

		[HttpGet]
		public ActionResult Register()
		{
			return View("Create", new UsersViewModel());
		}

		[HttpPost]
		public ActionResult Register(UsersViewModel request)
		{
			var user = request.Save(db);
			var userJson = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(user);

			var httpRequest = WebRequest.CreateHttp("http://localhost:27433/api/session/CreateUser");
			httpRequest.Method = "POST";
			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(userJson);
				streamWriter.Flush();
				streamWriter.Close();

				var httpResponse = (HttpWebResponse) httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
				}
			}

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult UsersList()
		{
			var httpRequest = WebRequest.CreateHttp("http://localhost:20754/api/apiusers/GetUsersList");
			httpRequest.Method = "GET";

			var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			string resp = "";

			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				resp = streamReader.ReadToEnd();
			}
			var users = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<string>>(resp);

			return View("UsersList", users);
		}

		[HttpGet]
		public ActionResult UsersWithTicketsList()
		{
			var userid = SessionHelper.GetAuthorizedUser(Session["sessionid"]);
			if (userid == 0)
				return RedirectToAction("Index", "Home");
			var httpRequest = WebRequest.CreateHttp("http://localhost:20754/api/apiusers/GetUsers");
			httpRequest.Method = "GET";

			var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			string resp = "";

			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				resp = streamReader.ReadToEnd();
			}
			var users = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<User>>(resp);

			var result = new Dictionary<String, string>();
			foreach (var user in users)
			{
				httpRequest = WebRequest.CreateHttp("http://localhost:47751/api/apitickets/LastUserTicketArtistName?userid=" + user.Id);
				httpRequest.Method = "GET";

				httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				var artistName = "";

				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					artistName = streamReader.ReadToEnd();
				}
				result.Add(user.Login,artistName);
				
			}
			return View("UsersWithTicketsList", result);
		}

		[HttpGet]
		public ActionResult Authorize()
		{
			return View("Authorize");
		}

		[HttpPost]
		public ActionResult Authorize(UsersViewModel request)
		{
			var client = new WebClient();
			var resp = client.DownloadString(
				string.Format("http://localhost:27433/api/session/AuthoriseUser?login={0}&password={1}",
				request.UserName, request.Password));

			var sessionid = resp;
			if (!String.IsNullOrWhiteSpace(sessionid))
			{
				Session["sessionid"] = sessionid;
				TempData["infMessage"] = "success";
			}
			else
			{
				TempData["infMessage"] = "denied";
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult AboutMe()
		{
			if (Session["userid"] != null)
			{
				var userModel = new UsersViewModel();
				var id = (int) Session["userid"];
				userModel.LoadData(db.Users.Single(u => u.Id == id ));
				return View("AboutMe", userModel);
			}
			TempData["infMessage"] = "needAuthorize";

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Logout()
		{
			SessionHelper.Logout(Session["sessionid"]);
			return RedirectToAction("Index", "Home");

		}

		[HttpGet]
		public ActionResult AuthorizeApplication(string ClientId)
		{
			return View("AuthorizeApplication", (object)ClientId);
		}

		[HttpPost]
		public ActionResult AuthorizeApplicationSubmit(string ClientId)
		{
			if (Session["userid"] != null)
			{
				var ap = db.Applications.SingleOrDefault(a => a.ClientIdentifier == ClientId);
				var code = BitConverter.ToString(
					MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Session["userid"].ToString() + DateTime.Now.ToString()))).Replace("-",string.Empty);
				db.Tokens.AddOrUpdate(token => token.ApplicationId,
					new Token()
				{
					ApplicationId = ap.Id,
					UserId = (int) Session["userid"],
					Code = code
				});
				db.SaveChanges();
				TempData["infMessage"] = "application authorized";
				return Redirect(ap.RedirectUrl + "?code=" + code);
			}
			TempData["infMessage"] = "you need authorize";
			return RedirectToAction("Index", "Home");
		}
	}
}
