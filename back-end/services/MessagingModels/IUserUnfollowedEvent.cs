using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingModels
{
    public interface IUserUnfollowedEvent
    {
        string UserId { get; set; }
        string UnfollowedUserId { get; set; }
    }
}
