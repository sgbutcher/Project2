using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project2.Models
{
    public class Blurb
    {
        [Key]
        public int BlurbID { get; set; }
        public string TKeyID { get; set; }
        public string UserID { get; set; }
        public DateTime Date { get; set; } 
        public string Title { get; set; }
        public string Body { get; set; }
        public string ReplyID { get; set; }
    }
}
