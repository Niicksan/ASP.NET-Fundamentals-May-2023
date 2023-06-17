namespace TaskBoard.Services.Interfaces
{
    using TaskBoard.ViewModels.Task;

    public interface ITaskService
    {
        Task AddAsync(string ownerId, TaskFormModel viewModel);

        Task<TaskDetailsViewModel> GetForDetailsByIdAsync(int id);
    }
}
