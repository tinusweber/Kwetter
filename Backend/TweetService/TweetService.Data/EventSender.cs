using MassTransit;
using MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetService.Data.Models;

namespace TweetService.Data
{
    public class EventSender : IEventSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendTweetTWeetedEvent(Tweet tweet)
        {
            await _publishEndpoint.Publish<ITweetTweetedEvent>(new
            {
                Id = tweet.Id,
                Tweeter = tweet.TweeterId,
                Body = tweet.Body,
            });
        }
    }
}
