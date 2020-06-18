using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace FeastFreedom.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string Email { get; set; }      
        public string Password { get; set; }
        public string BillingAddress { get; set; }
        public string SecurityQuestionOne { get; set; }
        public string SecurityQuestionTwo { get; set; }
        public int RoleId { get; set; }

    }
}