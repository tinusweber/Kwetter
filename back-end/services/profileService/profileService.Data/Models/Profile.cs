using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Data.Models
{
    public class Profile
    {
        [Key]
        public Guid OwnerId { get; set; }

        public string Username { get; set; }
        public string ProfilePictureBase64 { get; set; }
        public string Biography { get; set; }
        public List<Guid> FollowingUsers { get; set; }
        public List<Guid> BlockedUsers { get; set; }
    }
}
