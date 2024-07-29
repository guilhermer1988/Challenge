using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Response;
using TaskManager.Infra.Interface;
using TaskManager.Infra.Repository;
using TaskManager.Service.Interface;

namespace TaskManager.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskService _taskService;
        public ProjectService(IProjectRepository projectRepository,
            ITaskService taskService)
        {
            _projectRepository = projectRepository;
            _taskService = taskService;
        }

        public async Task<TaskManagerHttpResponse<bool>> Create(Project project)
        {
            var result = new TaskManagerHttpResponse<bool> { Data = new bool() };
            try
            {
                result.Data = await _projectRepository.InsertAndSave(project);
            }
            catch (Exception ex)
            {
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.ErrorMessage = ex.Message;
                return result;
            }

            result.StatusCode = System.Net.HttpStatusCode.OK;
            return result;
        }

        public async Task<TaskManagerHttpResponse<bool>> Delete(int projectId)
        {
            var result = new TaskManagerHttpResponse<bool> { Data = new bool() };
            try
            {
                var taskPending = await _taskService.GetByProjectAndStatus(projectId, StatusTask.Pending);

                if (taskPending) 
                {
                    result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    result.ErrorMessage = "Erro ao remover Projeto. Existem Tarefas com Status Pendente, remova-as e tente novamente.";
                    return result;
                }

                result.Data = await _projectRepository.Delete(projectId);
            }
            catch (Exception ex)
            {
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.ErrorMessage = ex.Message;
                return result;
            }

            result.StatusCode = System.Net.HttpStatusCode.OK;
            return result;
        }

        public async Task<TaskManagerHttpResponse<IList<Project>>> Get(string userName)
        {
            var result = new TaskManagerHttpResponse<IList<Project>> { Data = new List<Project>() };

            try
            {
                var projectList = await _projectRepository.GetByUserName(userName);

                result.Data = projectList;
            }
            catch (Exception ex)
            {
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.ErrorMessage = ex.Message;
                return result;
            }

            result.StatusCode = System.Net.HttpStatusCode.OK;
            return result;
        }
    }
}
