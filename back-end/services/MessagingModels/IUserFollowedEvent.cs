using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingModels
{
    public interface IUserFollowedEvent
    {
        string UserId { get; set; }
        string FollowedUserId { get; set; }
    }
}
