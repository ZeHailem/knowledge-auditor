using KnowledgeAuditor.Api.Core.Models;
using KnowledgeAuditor.Api.Pipeline;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeAuditor.Api.Controllers
{
    [ApiController]
    [Route("audit")]
    public sealed class AuditController : ControllerBase
    {
        private readonly IGovernancePipeline _pipeline;

        public AuditController(IGovernancePipeline pipeline)
        {
            _pipeline = pipeline;
        }

        [HttpPost]
        public ActionResult<AuditDecision> Audit([FromBody] AuditRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                return BadRequest(new
                {
                    error = "Question must not be empty."
                });
            }

            var decision = _pipeline.Execute(request.Question);

            return Ok(decision);
        }
    }
}