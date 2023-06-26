namespace TaskBoard.Services.Interfaces
{
    using TaskBoard.ViewModels.Task;

    public interface ITaskService
    {
        Task AddAsync(string ownerId, TaskFormModel viewModel);

        Task EditAsync(int taskId, TaskFormModel viewModel);

        Task DeleteAsync(int taskId);

        Task<TaskFormModel> GetTaskByIdForEditAsync(int taskId);

        Task<TaskViewModel> GetTaskByIdForDeleteAsync(int taskId);

        Task<TaskDetailsViewModel> GetForDetailsByIdAsync(int id);
    }
}
