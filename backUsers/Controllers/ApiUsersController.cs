using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using lab2.Models;

namespace lab2.Controllers
{
	public class ApiUsersController : ApiController
	{
		private MyShopDbContext db = new MyShopDbContext();
		
		[HttpGet]
		[ResponseType(typeof(List<string>))]
		public IHttpActionResult GetUsersList()
		{
			var users = db.Users.Select(u => u.Login);
			var result = Json(users);
			return result;
		}


		[HttpGet]
		[ResponseType(typeof(List<User>))]
		public IHttpActionResult GetUsers()
		{
			var users = db.Users;
			var result = Json(users);
			return result;
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
		public IHttpActionResult PostTicket()
		{
			var ticketJson = Request.Content.ReadAsStringAsync().Result;
			var ticket = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<Ticket>(ticketJson.ToString());

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Tickets.Add(ticket);
			db.SaveChanges();

			return Ok();
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