using STTechExcercise.Models;
using STTechExercise.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace STTechExercise
{
    public class ReimbursementTests
    {
        public ReimbursementTests() { 
            TestUtility.Setup(); 
        } 
        [Theory]
        [MemberData(nameof(ProjectData))]
        public void CalculateProjectReimbursement_Test(Project[] projects, int expected )
        {           
            var actual = TestUtility.projectReimbursementMock.CalculateProjectReimbursement(projects.ToList());
            Assert.Equal(expected, actual);
        }
        [Theory]
        [MemberData(nameof(DailyProject))]
        public void GetDailyReimbursement_Test(Project[] projects, DateTime currentDate, int expected)
        {
            var actual = TestUtility.projectReimbursementMock.GetDailyReimbursement(projects.ToList(), currentDate);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> ProjectData =>
      new List<object[]> {
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-03") }}, 165},
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-06") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-06"), EndDate = DateTime.Parse("2015-09-08") } }, 590},
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-03") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-05"), EndDate = DateTime.Parse("2015-09-07") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-08"), EndDate = DateTime.Parse("2015-09-08") } }, 445},
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-02") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-03"), EndDate = DateTime.Parse("2015-09-03") } }, 185},
      };
        public static IEnumerable<object[]> DailyProject =>
      new List<object[]> {
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-03") }}, DateTime.Parse("2015-09-02"),  75},
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-03") }}, DateTime.Parse("2015-09-01"),  45},
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-03") }}, DateTime.Parse("2015-09-03"),  45},
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-06") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-06"), EndDate = DateTime.Parse("2015-09-08") } }, DateTime.Parse("2015-09-02"), 85 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-06") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-06"), EndDate = DateTime.Parse("2015-09-08") } }, DateTime.Parse("2015-09-06"), 85 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-06") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-06"), EndDate = DateTime.Parse("2015-09-08") } }, DateTime.Parse("2015-09-07"), 75 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-06") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-06"), EndDate = DateTime.Parse("2015-09-08") } }, DateTime.Parse("2015-09-08"), 45 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-03") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-05"), EndDate = DateTime.Parse("2015-09-07") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-08"), EndDate = DateTime.Parse("2015-09-08") } }, DateTime.Parse("2015-09-03"), 45 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-03") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-05"), EndDate = DateTime.Parse("2015-09-07") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-08"), EndDate = DateTime.Parse("2015-09-08") } }, DateTime.Parse("2015-09-05"), 55 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-03") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-05"), EndDate = DateTime.Parse("2015-09-07") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-08"), EndDate = DateTime.Parse("2015-09-08") } }, DateTime.Parse("2015-09-02"), 75 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-02") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-03"), EndDate = DateTime.Parse("2015-09-03") } }, DateTime.Parse("2015-09-01"), 45 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-02") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-03"), EndDate = DateTime.Parse("2015-09-03") } }, DateTime.Parse("2015-09-02"), 85 },
                new object[] {new Project[] {new Project{ HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = false, StartDate = DateTime.Parse("2015-09-01"), EndDate = DateTime.Parse("2015-09-01") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-02"), EndDate = DateTime.Parse("2015-09-02") }, new Project { HighCostFlag = true, StartDate = DateTime.Parse("2015-09-03"), EndDate = DateTime.Parse("2015-09-03") } }, DateTime.Parse("2015-09-03"), 55 },
      };
    }
}
