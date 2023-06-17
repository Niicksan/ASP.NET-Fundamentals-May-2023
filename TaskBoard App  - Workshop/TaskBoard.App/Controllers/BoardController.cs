namespace TaskBoard.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskBoard.Services.Interfaces;
    using TaskBoard.ViewModels.Board;

    public class BoardController : Controller
    {
        private readonly IBoardService boardService;

        public BoardController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<BoardAllViewModel> allBoards = await this.boardService.AllAsync();

            return View(allBoards);
        }
    }
}
