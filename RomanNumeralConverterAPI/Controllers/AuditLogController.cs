using LogDBContext;
using LoggingRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RomanNumeralConverterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IRepository<RMLoggingModel> _logRepo;

        public AuditLogController(IRepository<RMLoggingModel> logRepo)
        {
            _logRepo = logRepo;
        }

        [HttpGet("ViewLog")]
        [SwaggerOperation(
        Summary = "View the Audit Log",
        Description = "Shows all log entries"
            )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ViewLog()
        {
            try
            {
               
                var logs = await _logRepo.GetAllAsync();
                logs = logs.OrderBy(x => x.CreatedDate);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
