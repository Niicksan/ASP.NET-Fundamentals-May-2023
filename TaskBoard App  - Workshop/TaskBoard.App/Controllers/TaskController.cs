namespace TaskBoard.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskBoard.App.Extensions;
    using TaskBoard.Services.Interfaces;
    using TaskBoard.ViewModels.Task;

    public class TaskController : Controller
    {
        private readonly IBoardService boardService;
        private readonly ITaskService taskService;

        public TaskController(IBoardService boardService, ITaskService taskService)
        {
            this.boardService = boardService;
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel viewModel = new TaskFormModel()
            {
                AllBoards = await this.boardService.AllForSelectAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskFormModel)
        {
            if (!ModelState.IsValid)
            {
                taskFormModel.AllBoards = await this.boardService.AllForSelectAsync();
                return this.View(taskFormModel);
            }

            bool isBoardExists = await this.boardService.ExistsByIdAsync(taskFormModel.BoardId);
            if (!isBoardExists)
            {
                ModelState.AddModelError(nameof(taskFormModel.BoardId), "Selected board does not exost!");
                taskFormModel.AllBoards = await this.boardService.AllForSelectAsync();
                return View(taskFormModel);
            }

            string userId = this.User.GetId();
            await this.taskService.AddAsync(userId, taskFormModel);

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                TaskDetailsViewModel viewModel = await this.taskService.GetForDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {

                return this.RedirectToAction("All", "Board");
            }
        }
    }
}
