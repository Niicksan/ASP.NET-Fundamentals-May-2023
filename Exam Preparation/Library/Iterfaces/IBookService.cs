using Library.Models;

namespace Library.Iterfaces
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
        Task AddBookAsync(AddBookViewModel model);
        Task EditBookAsync(AddBookViewModel model, int id);
        Task<BookViewModel?> GetBookByIdAsync(int id);
        Task<AddBookViewModel?> GetBookByIdForEditAsync(int id);
        Task AddBookToCollectionAsync(string userId, BookViewModel book);
        Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId);
        Task<AddBookViewModel> GetNewAddBookModelAsync();
        Task RemoveBookFromCollectionAsync(string userId, BookViewModel book);
    }
}