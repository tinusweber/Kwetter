using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetService.Data.Models
{
    public class Tweet
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TweeterId { get; set; }
        public string Body { get; set; }
        public DateTime TweetedAt { get; set; }
        
        public string ImageBase64 { get; set; }
    }
}
