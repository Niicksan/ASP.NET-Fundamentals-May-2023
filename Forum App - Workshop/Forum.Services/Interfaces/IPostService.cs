namespace Forum.Services.Interfaces
{
    using ViewModels.Post;

    public interface IPostService
    {
        Task<IEnumerable<PostListViewModel>> ListAllAsync();

        Task AddPostAsync(PostFormModel postFormModel);

        Task<PostFormModel> GetForEditOrDeleteByIdAsync(int id);

        Task EditBbyIdAsync(int id, PostFormModel postFormModel);

        Task DeleteByIdAsync(int id);
    }
}
