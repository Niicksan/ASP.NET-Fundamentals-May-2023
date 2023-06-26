namespace TaskBoard.App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskBoard.App.Extensions;
    using TaskBoard.Services.Interfaces;
    using TaskBoard.ViewModels.Task;

    [Authorize]
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            TaskFormModel taskToEdit = await taskService.GetTaskByIdForEditAsync(id);

            if (taskToEdit == null)
            {
                return RedirectToAction("All", "Board");
            }

            taskToEdit.AllBoards = await this.boardService.AllForSelectAsync();

            string currentUser = this.User.GetId();
            if (currentUser != taskToEdit.OwnerId)
            {
                return Unauthorized();
            }

            return View(taskToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormModel taskFormModel)
        {
            TaskFormModel taskToEdit = await taskService.GetTaskByIdForEditAsync(id);

            if (taskToEdit == null)
            {
                return RedirectToAction("All", "Board");
            }

            string currentUser = this.User.GetId();
            if (currentUser != taskToEdit.OwnerId)
            {
                return Unauthorized();
            }

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

            await taskService.EditAsync(id, taskFormModel);

            return this.RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            TaskViewModel taskToDelete = await taskService.GetTaskByIdForDeleteAsync(id);

            if (taskToDelete == null)
            {
                return RedirectToAction("All", "Board");
            }

            string currentUser = this.User.GetId();
            if (currentUser != taskToDelete.OwnerId)
            {
                return Unauthorized();
            }

            return View(taskToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, TaskViewModel taskFormModel)
        {
            TaskViewModel taskToDelete = await taskService.GetTaskByIdForDeleteAsync(id);

            if (taskToDelete == null)
            {
                return RedirectToAction("All", "Board");
            }

            string currentUser = this.User.GetId();
            if (currentUser != taskToDelete.OwnerId)
            {
                return Unauthorized();
            }

            await taskService.DeleteAsync(id);

            return this.RedirectToAction("All", "Board");
        }
    }
}
