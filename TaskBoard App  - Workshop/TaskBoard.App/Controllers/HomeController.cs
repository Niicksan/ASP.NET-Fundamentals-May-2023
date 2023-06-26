namespace TaskBoard.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskBoard.App.Extensions;
    using TaskBoard.Data;
    using TaskBoard.ViewModels.Home;


    public class HomeController : Controller
    {
        private readonly TaskBoardDbContext dbContext;

        public HomeController(TaskBoardDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var tasksBoards = this.dbContext
                .Boards
                .Select(b => b.Name)
                .Distinct()
                .ToList();

            var tasksCounts = new List<HomeBoardModel>();
            foreach (var boardName in tasksBoards)
            {
                int tasksInBoard = this.dbContext.Tasks.Where(t => t.Board.Name == boardName).Count();
                tasksCounts.Add(new HomeBoardModel()
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            int userTasksCount = -1;

            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = User.GetId();
                userTasksCount = this.dbContext.Tasks.Where(t => t.OwnerId == currentUserId).Count();
            }

            HomeViewModel homeViewModel = new HomeViewModel()
            {
                AllTasksCount = this.dbContext.Tasks.Count(),
                BoardsWithTasksCount = tasksCounts,
                UserTasksCount = userTasksCount
            };

            return View(homeViewModel);
        }
    }
}