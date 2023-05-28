using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Introduction___Exercise.Controllers
{
    using ASP.NET_Core_Introduction___Exercise.ViewModels.TextSplit;

    public class TextSplitController : Controller
    {
        [HttpGet]
        [Route("/text-spliter")]
        public IActionResult TextSplit(TextSplitViewModel textViewModel)
        {
            return View(textViewModel);
        }

        [HttpPost]
        public IActionResult Split(TextSplitViewModel textViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("TextSplit", textViewModel);
            }

            string[] words = textViewModel
                .TextToSplit
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string splitText = String.Join(Environment.NewLine, words);
            textViewModel.SplitText = splitText;

            return this.RedirectToAction("TextSplit", textViewModel);
        }
    }
}
