namespace TaskBoard.Services.Interfaces
{
    using TaskBoard.ViewModels.Board;

    public interface IBoardService
    {
        Task<IEnumerable<BoardAllViewModel>> AllAsync();

        Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync();

        Task<bool> ExistsByIdAsync(int id);
    }
}
