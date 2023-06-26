namespace TaskBoard.Services
{
    using Microsoft.EntityFrameworkCore;
    using TaskBoard.Data;
    using TaskBoard.Services.Interfaces;
    using TaskBoard.ViewModels.Board;
    using TaskBoard.ViewModels.Task;

    public class BoardService : IBoardService
    {
        private readonly TaskBoardDbContext dbContext;

        public BoardService(TaskBoardDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BoardAllViewModel>> AllAsync()
        {
            IEnumerable<BoardAllViewModel> allBoards = await this.dbContext
                .Boards
                .Select(b => new BoardAllViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b
                        .Tasks
                        .Select(t => new TaskViewModel()
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Owner = t.Owner.UserName
                        })
                        .ToArray()
                })
                .ToArrayAsync();

            return allBoards;
        }

        public async Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync()
        {
            IEnumerable<BoardSelectViewModel> allBoards = await this.dbContext
                .Boards
                .Select(b => new BoardSelectViewModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToArrayAsync();

            return allBoards;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Boards
                .AnyAsync(b => b.Id == id);

            return result;
        }
    }
}