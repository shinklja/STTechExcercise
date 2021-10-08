using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using STTechExcercise.Configuration;
using STTechExcercise.Services;

namespace STTechExercise.Test
{
    public static class TestUtility
    {
        public static ProjectReimbursementService projectReimbursementMock;
        public static ReimbursementValuesConfiguration config;
        public static void Setup()
        {
            var configMock = new Mock<ReimbursementValuesConfiguration>();
            configMock.Setup(p => p.HighCostPay).Returns(85);
            configMock.Setup(p => p.HighCostTravel).Returns(55);
            configMock.Setup(p => p.LowCostPay).Returns(75);
            configMock.Setup(p => p.LowCostTravel).Returns(45);
            config = configMock.Object;

            projectReimbursementMock = new ProjectReimbursementService(new NullLogger<ProjectReimbursementService>(), TestUtility.config);
        }
    }
}
