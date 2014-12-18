using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using lab2.Models;

namespace lab2.Controllers
{
	public class SessionController : ApiController
	{
		private MyShopDbContext db = new MyShopDbContext();
		private static List<User> authorizedUsers;

		public SessionController() : base()
		{
			if (authorizedUsers == null)
				authorizedUsers = new List<User>();
		}

		[HttpGet]
		public IHttpActionResult AuthoriseUser(string login, string password)
		{
			var passwordHash = password.GetHashCode().ToString();
			var user = db.Users.SingleOrDefault(u => u.Login == login && u.PasswordHash == passwordHash);
			if (user == null)
				return BadRequest(); // 501 access denied
			var result = user.Login.GetHashCode();
			authorizedUsers.Add(user);

			
			return Json(result);
		}

		[HttpPost]
		public IHttpActionResult CreateUser()
		{
			var userJson = Request.Content.ReadAsStringAsync().Result;
			
			var user = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<User>(userJson.ToString());
			db.Users.Add(user);
			db.SaveChanges();
			return Ok();
		}

		[HttpGet]
		public IHttpActionResult GetAuthorizedUserId(string sessionCode)
		{
			var user = authorizedUsers.SingleOrDefault(u => u.Login.GetHashCode().ToString() == sessionCode);

			if (user == null)
				return BadRequest();
			return Json(user.Id);
		}
		[HttpPost]
		public IHttpActionResult LogoutUser(string sessionCode)
		{
			var user = authorizedUsers.SingleOrDefault(u => u.Login.GetHashCode().ToString() == sessionCode);

			
			if (user == null)
				return BadRequest();
			authorizedUsers.RemoveAll(u => u.Login.GetHashCode().ToString() == sessionCode);
			return Json(user.Id);
		}

		// GET api/ApiTickets/5
		//[ResponseType(typeof(Ticket))]
		//public IHttpActionResult GetTicket(int id, string token)
		//{
		//	var tokenRow = db.Tokens.SingleOrDefault(t => t.TokenString == token);
		//	if (tokenRow == null || tokenRow.Expired())
		//		return BadRequest();ы
		//	var userid = tokenRow.UserId;
		//	var ticket = db.Tickets.Where(t => t.UserId == userid).SingleOrDefault(t => t.Id == id);
		//	if (ticket == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(ticket);
		//}
		[HttpGet]
		[ResponseType(typeof(Ticket))]
		public IHttpActionResult GetOneTicket(int id)
		{
			var ticket = db.Tickets.SingleOrDefault(t => t.Id == id );
			if (ticket == null)
			{
				return BadRequest();
			}

			return Ok(ticket);
		}

		// PUT api/ApiTickets/5
		public IHttpActionResult PutTicket(int id, Ticket ticket)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != ticket.Id)
			{
				return BadRequest();
			}

			db.Entry(ticket).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TicketExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST api/ApiTickets
		[ResponseType(typeof(Ticket))]
		public IHttpActionResult PostTicket(Ticket ticket)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Tickets.Add(ticket);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = ticket.Id }, ticket);
		}

		// DELETE api/ApiTickets/5
		[ResponseType(typeof(Ticket))]
		public IHttpActionResult DeleteTicket(int id)
		{
			Ticket ticket = db.Tickets.Find(id);
			if (ticket == null)
			{
				return NotFound();
			}

			db.Tickets.Remove(ticket);
			db.SaveChanges();

			return Ok(ticket);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool TicketExists(int id)
		{
			return db.Tickets.Count(e => e.Id == id) > 0;
		}
	}
}