namespace TaskBoard.Services
{
    using Microsoft.EntityFrameworkCore;
    using TaskBoard.Data;
    using TaskBoard.Services.Interfaces;
    using TaskBoard.ViewModels.Task;

    public class TaskService : ITaskService
    {
        private readonly TaskBoardDbContext dbContext;

        public TaskService(TaskBoardDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(string ownerId, TaskFormModel viewModel)
        {
            TaskBoard.Data.Models.Task task = new TaskBoard.Data.Models.Task()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                BoardId = viewModel.BoardId,
                CreatedOn = DateTime.UtcNow,
                OwnerId = ownerId
            };

            await this.dbContext.Tasks.AddAsync(task);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(int taskId, TaskFormModel viewModel)
        {
            var taskToEdit = await this.dbContext.Tasks.FindAsync(taskId);

            if (taskToEdit != null)
            {
                taskToEdit.Title = viewModel.Title;
                taskToEdit.Description = viewModel.Description;
                taskToEdit.BoardId = viewModel.BoardId;


                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int taskId)
        {
            var taskToDelete = await this.dbContext
               .Tasks
               .FirstAsync(t => t.Id == taskId);

            this.dbContext.Remove(taskToDelete);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<TaskFormModel?> GetTaskByIdForEditAsync(int id)
        {
            return await this.dbContext.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskFormModel
                {
                    Title = t.Title,
                    Description = t.Description,
                    BoardId = t.BoardId,
                    OwnerId = t.OwnerId,
                }).FirstOrDefaultAsync();
        }

        public async Task<TaskViewModel?> GetTaskByIdForDeleteAsync(int taskId)
        {
            return await this.dbContext.Tasks
                  .Where(t => t.Id == taskId)
                  .Select(t => new TaskViewModel
                  {
                      Title = t.Title,
                      Description = t.Description,
                      OwnerId = t.OwnerId,
                  }).FirstOrDefaultAsync();
        }

        public async Task<TaskDetailsViewModel> GetForDetailsByIdAsync(int id)
        {
            TaskDetailsViewModel viewModel = await this.dbContext
                .Tasks
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Owner = t.Owner.UserName,
                    CreatedOn = t.CreatedOn.ToString("f"),
                    Board = t.Board.Name
                })
                .FirstAsync(t => t.Id == id);

            return viewModel;
        }
    }
}