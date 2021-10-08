using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using STTechExercise.Models;
using STTechExercise.Services;
using System.Collections.Generic;
using System.Linq;

namespace STTechExercise.Controllers
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
            
            if (projects != null && projects.Count() >0 && ValidateProjects(projects))
            {
                _logger.LogInformation($"Projects to reimburse : {JsonConvert.SerializeObject(projects)}");
                var reimbursement = _projectReimbursementService.CalculateProjectReimbursement(projects);
                _logger.LogInformation($"Reimbursement Value :{reimbursement}");
                return Ok(reimbursement);
            }
            else
            {
                _logger.LogError($"There weren't any projects to process.");
                return BadRequest();
            }

        }
        private bool ValidateProjects(List<Project> projects)
        {
            if (projects.FirstOrDefault(f => !f.EndDate.HasValue || !f.StartDate.HasValue || !f.HighCostFlag.HasValue) != null)
                return false;
            return true;
        }
    }
}
