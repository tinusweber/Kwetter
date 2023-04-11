using Helpers;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Models;
using ReportService.Application;
using ReportService.DomainModels;

namespace ReportService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportApp _app;

        public ReportController(ReportApp app)
        {
            _app = app;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var reports = this._app.GetAll();
            return Ok(reports);
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await this._app.GetById(Guid.Parse(id)));
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await this._app.DeleteById(Guid.Parse(id));
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddReportRequest req)
        {
            var res = await this._app.Add(new Report(new Guid(),Guid.Parse(req.PostId), HttpContext.GetUserId(), req.Reason));

            return Ok(res);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateReportRequest req)
        {
            await this._app.Update(
                Guid.Parse(id),
                (ReportStatus)req.ReportStatus,
                req.Reason);

            return Ok();
        }
    }
}