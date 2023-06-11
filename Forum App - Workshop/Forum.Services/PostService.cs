namespace Forum.Services
{
    using Forum.Data;
    using Forum.Data.Models;
    using Forum.Services.Interfaces;
    using Forum.ViewModels.Post;
    using Microsoft.EntityFrameworkCore;

    public class PostService : IPostService
    {
        private readonly ForumDbContext dbContext;

        public PostService(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PostListViewModel>> ListAllAsync()
        {
            IEnumerable<PostListViewModel> allPosts = await dbContext
                .Posts
                .Select(p => new PostListViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                })
                .ToArrayAsync();

            return allPosts;
        }

        public async Task AddPostAsync(PostFormModel postFormModel)
        {
            Post newPost = new Post()
            {
                Title = postFormModel.Title,
                Content = postFormModel.Content
            };

            await this.dbContext.Posts.AddAsync(newPost);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<PostFormModel> GetForEditOrDeleteByIdAsync(int id)
        {
            Post postToEdit = await this.dbContext
                .Posts
                .FirstAsync(p => p.Id == id);

            return new PostFormModel()
            {
                Title = postToEdit.Title,
                Content = postToEdit.Content
            };
        }

        public async Task EditBbyIdAsync(int id, PostFormModel postEditedModel)
        {
            Post postToEdit = await this.dbContext
                .Posts
                .FirstAsync(p => p.Id == id);

            postToEdit.Title = postEditedModel.Title;
            postToEdit.Content = postEditedModel.Content;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            Post postToDelete = await this.dbContext
               .Posts
               .FirstAsync(p => p.Id == id);

            this.dbContext.Remove(postToDelete);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
