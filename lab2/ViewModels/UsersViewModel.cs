using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using lab2.Models;
using Newtonsoft.Json;

namespace lab2.ViewModels
{
	public class UsersViewModel
	{
		public bool ShowEmail { get; set; }
		
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string UserName { get; set; }

		public User Save(MyShopDbContext context)
		{
			var user = new User
			{
				Email = Email,
				PasswordHash = Password.GetHashCode().ToString(),
				Login = UserName
			};
			return user;
		}

		public string Authorize(MyShopDbContext context)
		{
			var passHash = Password.GetHashCode().ToString();
			var user = context.Users.SingleOrDefault(u => u.Login == UserName && u.PasswordHash == passHash);
			if (user != null)
			{
				return (UserName + DateTime.UtcNow).GetHashCode().ToString();
			}
			return null;
		}

		public void LoadData(User user)
		{
			Email = user.Email;
			UserName = user.Login;
		}
	}
}