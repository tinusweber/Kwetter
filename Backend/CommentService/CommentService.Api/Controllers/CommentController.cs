using CommentService.Api.Models;
using CommentService.Application;
using CommentService.DomainModels;
using Helpers;
using Mapster;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommentService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentApp _app;

        public CommentController(CommentApp app)
        {
            _app = app;
        }


        // GET api/<CommentController>
        [HttpGet]
        public IActionResult Get(string? tweetId = null, string? userId = null)
        {
            var comments = this._app.GetFiltered(tweetId, userId);
            return Ok(comments);
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await this._app.GetByIdAsync(Guid.Parse(id)));
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommentRequest value)
        {
            var result = await this._app.CreateAsync(value.Adapt<Comment>(), HttpContext.GetUserId());
            return Ok(result);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
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