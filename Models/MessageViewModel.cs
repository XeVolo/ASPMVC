using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Conversation { get; set; }
        public string Message { get; set; }
        
    }
}