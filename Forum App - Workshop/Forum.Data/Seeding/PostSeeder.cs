namespace Forum.Data.Seeding
{
    using Models;

    public class PostSeeder
    {
        internal Post[] GeneratePosts()
        {
            ICollection<Post> posts = new HashSet<Post>();

            Post firstPost = new Post()
            {
                Id = 1,
                Title = "My first post",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur vitae dignissim nisi. Fusce vitae gravida nibh. Mauris in convallis metus."
            };
            posts.Add(firstPost);

            Post secondPost = new Post()
            {
                Id = 2,
                Title = "My second post",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc consectetur elit vitae metus eleifend porta. Mauris id diam ultricies, ultrices."
            };
            posts.Add(secondPost);

            Post thirdPost = new Post()
            {
                Id = 3,
                Title = "My third post",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec hendrerit, nunc eu interdum ultricies, quam massa viverra elit, luctus dapibus."
            };
            posts.Add(thirdPost);

            return posts.ToArray();
        }
    }
}
