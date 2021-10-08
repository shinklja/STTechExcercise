using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STTechExcercise.Models;
using STTechExcercise.Services;
using System.Collections.Generic;
using System.Linq;

namespace STTechExcercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReimbursementController : ControllerBase
    {
        private readonly ILogger<ReimbursementController> _logger;
        private readonly ProjectReimbursementService _projectReimbursementService;
        public ReimbursementController(ILogger<ReimbursementController> logger, ProjectReimbursementService projectReimbursementService)
        {
            _logger = logger;
            _projectReimbursementService = projectReimbursementService;
        }

        [HttpPost]
        public ActionResult<int> GetReimbursment([FromBody] List<Project> projects)
        {
            if (projects != null && projects.Count() >0)
            {
                var reimbursement = _projectReimbursementService.CalculateProjectReimbursement(projects);
                return Ok(reimbursement);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
