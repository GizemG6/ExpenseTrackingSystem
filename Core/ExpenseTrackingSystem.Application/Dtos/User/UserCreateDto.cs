﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.User
{
    public class UserCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
		public string Password { get; set; }
		public string IBAN { get; set; }
        public string PhoneNumber { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
