using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportService.Api.Models;
using ReportService.Application;
using ReportService.DomainModels;
using System;
using System.Threading.Tasks;

namespace ReportService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportApp _app;
        private readonly ILogger<ReportController> _logger;

        public ReportController(ReportApp app, ILogger<ReportController> logger)
        {
            _app = app;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get() method called.");

            var reports = this._app.GetAll();
            return Ok(reports);
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(string id)
        {
            _logger.LogInformation("GetById() method called with ID: {Id}", id);

            return Ok(await this._app.GetById(Guid.Parse(id)));
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Delete() method called with ID: {Id}", id);

            await this._app.DeleteById(Guid.Parse(id));
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddReportRequest req)
        {
            _logger.LogInformation("Add() method called.");

            var res = await this._app.Add(new Report(new Guid(), Guid.Parse(req.PostId), HttpContext.GetUserId(), req.Reason));

            return Ok(res);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateReportRequest req)
        {
            _logger.LogInformation("Update() method called with ID: {Id}", id);

            await this._app.Update(
                Guid.Parse(id),
                (ReportStatus)req.ReportStatus,
                req.Reason);

            return Ok();
        }
    }
}
