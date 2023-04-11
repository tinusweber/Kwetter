using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentService.Data.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CommenterId { get; set; }
        public Guid TweetId { get; set; }
        public string Body { get; set; }
    }
}
