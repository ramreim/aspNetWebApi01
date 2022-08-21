using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication14.Models
{
    public class History
    {

        public int EventId { get; set; } = 0;
        public string FirstName { get; set; } = "";
        public int CustomerId { get; set; } = 0;

        public string Status { get; set; } = "";

        public string DateTimee { get; set; } = "";
    }
}