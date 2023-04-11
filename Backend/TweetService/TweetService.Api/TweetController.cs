using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetService.Api.Models;
using TweetService.Api.Models.Requests;
using TweetService.Application;

namespace TweetService.Api
{
    [ApiController]
    [Route("[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly TweetApplication tweetApp;

        public TweetController(TweetApplication tweetApp)
        {
            this.tweetApp = tweetApp;
        }

        [HttpGet("/Feed")]
        public IActionResult GetFeed()
        {
            var guid = this.HttpContext.GetUserId();
            var result = this.tweetApp.GetFeedByUser(guid);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                return Ok(this.tweetApp.GetTweetByUser(Guid.Parse(userId!)));
            }
            var result = this.tweetApp.GetTweetByUser(HttpContext.GetUserId());
            return Ok(result);
        }

        [HttpGet("{tweetId}", Name = nameof(GetById))]
        public IActionResult GetById([FromRoute] string tweetId)
        {
            var result = this.tweetApp.GetTweetById(Guid.Parse(tweetId));

            if (result.TweeterId == HttpContext.GetUserId()
                || HttpContext.GetUserRole() == Role.Admin)
            {
                return Ok(result);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostTweetRequest postRequest)
        {
            var result = await tweetApp.AddTweet(postRequest.AsDomainModel(HttpContext.GetUserId()));
            return CreatedAtRoute(nameof(GetById), new { tweetId = result.Id }, result);
        }

        [HttpDelete("{tweetId}")]
        public async Task<IActionResult> Delete([FromRoute] string tweetId)
        {
            var guid = Guid.Parse(tweetId);
            var result = this.tweetApp.GetTweetById(guid);

            if (result.TweeterId == HttpContext.GetUserId()
                || HttpContext.GetUserRole() == Role.Admin)
            {
                await tweetApp.DeleteTweet(result.Id);
                return NoContent();
            }

            return Unauthorized();
        }
    }
}
