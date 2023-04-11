using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetService.Data.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public List<Guid> Following { get; set; }
    }
}
