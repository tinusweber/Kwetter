using CommentService.Api.Models;
using CommentService.Application;
using CommentService.DomainModels;
using Helpers;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Add this namespace

namespace CommentService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentApp _app;
        private readonly ILogger<CommentController> _logger; // Add ILogger

        public CommentController(CommentApp app, ILogger<CommentController> logger) // Inject ILogger
        {
            _app = app;
            _logger = logger;
        }

        // GET api/<CommentController>
        [HttpGet]
        public IActionResult Get(string? tweetId = null, string? userId = null)
        {
            _logger.LogInformation("Get comments"); // Log information
            var comments = this._app.GetFiltered(tweetId, userId);
            return Ok(comments);
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(string id)
        {
            _logger.LogInformation($"Get comment by ID: {id}"); // Log information
            return Ok(await this._app.GetByIdAsync(Guid.Parse(id)));
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommentRequest value)
        {
            _logger.LogInformation("Create comment"); // Log information
            var result = await this._app.CreateAsync(value.Adapt<Comment>(), HttpContext.GetUserId());
            return Ok(result);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation($"Delete comment by ID: {id}"); // Log information
            var result = await this._app.GetByIdAsync(Guid.Parse(id));

            if (result.CommenterId == HttpContext.GetUserId()
                || HttpContext.GetUserRole() == Role.Admin)
            {
                await this._app.DeleteByIdAsync(Guid.Parse(id));
                return NoContent();
            }

            return Unauthorized();
        }
    }
}
