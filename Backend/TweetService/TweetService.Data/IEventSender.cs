using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetService.Data.Models;

namespace TweetService.Data
{
    public interface IEventSender
    {
        Task SendTweetTWeetedEvent(Tweet package);

    }
}
