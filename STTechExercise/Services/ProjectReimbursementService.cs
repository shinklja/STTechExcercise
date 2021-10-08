using Microsoft.Extensions.Logging;
using STTechExcercise.Configuration;
using STTechExcercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace STTechExcercise.Services
{
    public class ProjectReimbursementService  
    {
        private readonly ILogger<ProjectReimbursementService> _logger;
        private readonly ReimbursementValuesConfiguration _reimbursementValues;
        public ProjectReimbursementService(ILogger<ProjectReimbursementService> logger, ReimbursementValuesConfiguration reimbursementValues)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _reimbursementValues = reimbursementValues ?? throw new ArgumentNullException(nameof(reimbursementValues));
        }
        public int CalculateProjectReimbursement(List<Project> projects)
        {
            var currentDate = projects.Min(m => m.StartDate);
            var maxEndDate = projects.Max(m => m.EndDate);
            _logger.LogInformation($"Min StartDate : {currentDate.ToShortDateString()}");
            _logger.LogInformation($"Max EndDate : {maxEndDate.ToShortDateString()}");
            var totalReimbursement = 0;
            while(currentDate <= maxEndDate)
            {
                totalReimbursement += GetDailyReimbursement(projects, currentDate);
                currentDate = currentDate.AddDays(1);
                _logger.LogDebug($"CurrentDate : {currentDate.ToShortDateString()}, Current TotalReimbursement : {totalReimbursement}");
            }
            return totalReimbursement;
        }
        public int GetDailyReimbursement(List<Project> projects, DateTime currentDate) {

            var projectsActiveDuringCurrentDate = projects.Where(w => w.StartDate <= currentDate && w.EndDate >= currentDate);
            if(projectsActiveDuringCurrentDate == null || projectsActiveDuringCurrentDate.Count() == 0)
            {
                _logger.LogDebug($"Gap Day");
                return 0;
            }
            if (projects.FirstOrDefault(w => w.StartDate <= currentDate.AddDays(1) && w.EndDate >= currentDate.AddDays(1)) != null && projects.FirstOrDefault(w => w.StartDate <= currentDate.AddDays(-1) && w.EndDate >= currentDate.AddDays(-1)) != null)
            {
                _logger.LogDebug($"High cost project day : {projectsActiveDuringCurrentDate.Max(m => m.HighCostFlag)}");
                return projectsActiveDuringCurrentDate.Max(m => m.HighCostFlag) ? _reimbursementValues.HighCostPay : _reimbursementValues.LowCostPay;
            }
            else
            {
                _logger.LogDebug($"High cost travel day : {projectsActiveDuringCurrentDate.Max(m => m.HighCostFlag)}");
                return projectsActiveDuringCurrentDate.Max(m => m.HighCostFlag) ? _reimbursementValues.HighCostTravel : _reimbursementValues.LowCostTravel;
            }
        }
    }
}
