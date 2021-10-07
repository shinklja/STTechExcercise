using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using STTechExcercise.Models;
using STTechExcercise.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        private bool ValidProjectJson(string projectJson)
        {
                JSchema schema = JSchema.Parse(System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Schemas\project-json-schema.json")));
                JObject project = JObject.Parse(projectJson);
                return project.IsValid(schema);
        }

    }
}
