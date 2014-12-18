using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using lab2.Models;
using lab2.ViewModels;

namespace lab2.Controllers
{
	public class TicketsController : Controller
	{
		[HttpGet]
		public ActionResult Ticket(int id)
		{
			var client = new WebClient();
			var resp = client.DownloadString("http://localhost:20754/api/apitickets/GetOneTicket?id=" + id);
			var ticket = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<Ticket>(resp);
			
			return View("OneTicket", ticket);
		}
	}
}
