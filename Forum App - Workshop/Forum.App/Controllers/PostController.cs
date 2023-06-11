namespace Forum.App.Controllers
{
    using Forum.Services.Interfaces;
    using Forum.ViewModels.Post;
    using Microsoft.AspNetCore.Mvc;

    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }


        [Route("/posts/all")]
        public async Task<IActionResult> All()
        {
            IEnumerable<PostListViewModel> allPost = await this.postService.ListAllAsync();

            return View(allPost);
        }

        [HttpGet]
        [Route("/posts/add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/posts/add")]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.postService.AddPostAsync(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while adding your post!");

                return View(model);
            }

            return RedirectToAction("All", "Post");
        }

        [HttpGet]
        [Route("/posts/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                PostFormModel postForEdit = await this.postService.GetForEditOrDeleteByIdAsync(id);

                return View(postForEdit);
            }
            catch (Exception)
            {
                return this.RedirectToAction("All", "Post");
            }
        }

        [HttpPost]
        [Route("/posts/edit/{id}")]
        public async Task<IActionResult> Edit(int id, PostFormModel editedModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editedModel);
            }

            try
            {
                await this.postService.EditBbyIdAsync(id, editedModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating your post!");

                return View(editedModel);
            }

            return RedirectToAction("All", "Post");
        }

        [HttpGet]
        [Route("/posts/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                PostFormModel postForDelete = await this.postService.GetForEditOrDeleteByIdAsync(id);

                return View(postForDelete);
            }
            catch (Exception)
            {
                return this.RedirectToAction("All", "Post");
            }
        }

        [HttpPost]
        [Route("/posts/delete/{id}")]
        public async Task<IActionResult> DeleteWithView(int id, PostFormModel postModel)
        {
            try
            {
                await this.postService.DeleteByIdAsync(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while deleting your post!");
                return this.View(postModel);
            }

            return RedirectToAction("All", "Post");
        }
    }
}
