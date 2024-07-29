using Microsoft.EntityFrameworkCore;
using Moq;
using System.Runtime.InteropServices;
using TaskManager.Domain.Entities;
using TaskManager.Infra.Data.Context;
using TaskManager.Infra.Interface;
using TaskManager.Service;
using TaskManager.Service.Interface;
using Xunit.Sdk;

namespace UnitTests
{
    public class ProjectServiceUnitTest
    {
        [Fact]
        public async System.Threading.Tasks.Task AddProjectSuccess()
        {
            var mockSet = new Mock<DbSet<Project>>();
            Mock<IProjectService> mockContext = new Mock<IProjectService>();
            Mock<IProjectRepository> mockProjectRepository = new Mock<IProjectRepository>();
            Mock<ITaskService> mockTaskService = new Mock<ITaskService>();

            // arrange
            Project project = new Project()
            {
                Title = "Test",
                Description = "Test",
                CreateAt = DateTime.Now,
                UserName = "usuario",
            };

            mockContext.Setup(m => m.Create(project))
                .ReturnsAsync(
                new TaskManager.Domain.Response.TaskManagerHttpResponse<bool>()
                { Data = true });

            ProjectService service = new ProjectService(mockProjectRepository.Object, mockTaskService.Object);
            var createResult = await mockContext.Object.Create(project);

            // assert
            Assert.True(createResult.Data);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteProjectSuccess()
        {
            var mockSet = new Mock<DbSet<Project>>();
            Mock<IProjectService> mockContext = new Mock<IProjectService>();
            Mock<IProjectRepository> mockProjectRepository = new Mock<IProjectRepository>();
            Mock<ITaskService> mockTaskService = new Mock<ITaskService>();

            // arrange
            int projectId = 1;

            mockContext.Setup(m => m.Delete(projectId))
                .ReturnsAsync(
                new TaskManager.Domain.Response.TaskManagerHttpResponse<bool>()
                { Data = true });

            ProjectService service = new ProjectService(mockProjectRepository.Object, mockTaskService.Object);

            // act
            var deleteResult = await mockContext.Object.Delete(projectId);

            // assert
            Assert.True(deleteResult.Data);
        }
    }
}