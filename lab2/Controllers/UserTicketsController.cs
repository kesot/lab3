using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lab2.Models;

namespace lab2.Controllers
{
	public class UserTicketsController : Controller
	{

		// GET: /UserTickets/
		public ActionResult Index()
		{
			var userid = SessionHelper.GetAuthorizedUser(Session["sessionid"]);
			if (userid == 0)
				return RedirectToAction("Authorize", "Users");
			
			var httpRequest = WebRequest.CreateHttp("http://localhost:47751/api/apitickets/GetTicketsForOwner?userid=" + userid);
			httpRequest.Method = "GET";

			var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			string resp = "";

			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				resp = streamReader.ReadToEnd();
			}
			var tickets = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<List<Ticket>>(resp);

			return View(tickets.ToList());
		}

		// GET: /UserTickets/Create
		public ActionResult Create()
		{

			return View();
		}

		// POST: /UserTickets/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Ticket ticket)
		{
			var currentUserId = SessionHelper.GetAuthorizedUser(Session["sessionid"]);
			if (currentUserId == 0)
				return RedirectToAction("Authorize", "Users");
			ticket.UserId = currentUserId;
			var ticketJson = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(ticket);

			var httpRequest = WebRequest.CreateHttp("http://localhost:47751/api/apitickets/PostTicket");
			httpRequest.Method = "POST";
			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(ticketJson);
				streamWriter.Flush();
				streamWriter.Close();

				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
				}
			}
			return RedirectToAction("Index");
		}

		// GET: /UserTickets/Edit/5
		public ActionResult Edit(int? id)
		{
			var currentUserId = SessionHelper.GetAuthorizedUser(Session["sessionid"]);
			if (currentUserId == 0)
				return RedirectToAction("Authorize", "Users");

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var client = new WebClient();
			var resp = client.DownloadString("http://localhost:47751/api/apitickets/GetOneTicket?id=" + id);
			var ticket = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<Ticket>(resp);

			if (ticket == null || ticket.UserId != currentUserId)
			{
				return HttpNotFound();
			}
			return View(ticket);
		}

		// POST: /UserTickets/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Ticket ticket)
		{
			var currentUserId = SessionHelper.GetAuthorizedUser(Session["sessionid"]);
			if (currentUserId == 0)
				return RedirectToAction("Authorize", "Users");
			ticket.UserId = currentUserId;
			var ticketJson = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(ticket);

			var httpRequest = WebRequest.CreateHttp("http://localhost:47751/api/apitickets/PutTicket");
			httpRequest.Method = "PUT";
			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(ticketJson);
				streamWriter.Flush();
				streamWriter.Close();

				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
				}
			}
			return RedirectToAction("Index");
		}

		//// GET: /UserTickets/Delete/5
		public ActionResult Delete(int? id)
		{

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var currentUserId = SessionHelper.GetAuthorizedUser(Session["sessionid"]);
			if (currentUserId == 0)
				return RedirectToAction("Authorize", "Users");

			var httpRequest = WebRequest.CreateHttp("http://localhost:47751/api/apitickets/DeleteTicket/"+id);
			httpRequest.Method = "DELETE";
			httpRequest.GetResponse();

			return RedirectToAction("Index");
		}
	}
}
