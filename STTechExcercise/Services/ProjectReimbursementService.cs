using Microsoft.Extensions.Logging;
using STTechExcercise.Configuration;
using STTechExcercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var totalReimbursement = 0;
            while(currentDate <= maxEndDate)
            {
                totalReimbursement += GetDailyReimbursement(projects, currentDate);
                currentDate = currentDate.AddDays(1);
            }
            return totalReimbursement;
        }
        public int GetDailyReimbursement(List<Project> projects, DateTime currentDate) {

            var projectsActiveDuringCurrentDate = projects.Where(w => w.StartDate <= currentDate && w.EndDate >= currentDate);
            if(projectsActiveDuringCurrentDate == null || projectsActiveDuringCurrentDate.Count() == 0)
            {
                return _reimbursementValues.HighCostTravel; //sure in a gap I could do low cost pay, but I'm all for worker's rights ;)
            }
            var middleProjects = projects.Where(w => w.StartDate < currentDate && w.EndDate > currentDate);
            if (middleProjects != null && middleProjects.Count() > 0)
            {
                return middleProjects.Max(m => m.HighCostFlag) ? _reimbursementValues.HighCostPay : _reimbursementValues.LowCostPay;
            }
            else if(projects.FirstOrDefault(w => w.StartDate <= currentDate.AddDays(1) && w.EndDate >= currentDate.AddDays(1)) != null)
            {
                return projectsActiveDuringCurrentDate.Max(m => m.HighCostFlag) ? _reimbursementValues.HighCostPay : _reimbursementValues.LowCostPay;
            }
            else
            {
                return projectsActiveDuringCurrentDate.Max(m => m.HighCostFlag) ? _reimbursementValues.HighCostTravel : _reimbursementValues.HighCostTravel;
            }
        }
    }
}
