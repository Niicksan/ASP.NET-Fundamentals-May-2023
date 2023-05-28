using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Introduction___Exercise.Controllers
{
    public class NumberController : Controller
    {
        [Route("/number/numbers")]
        public IActionResult Numbers()
        {
            return View();
        }

        [HttpGet]
        [Route("/number/numbers-to-n")]
        public IActionResult NumbersToN()
        {
            ViewBag.Count = 0;
            return View();
        }

        [HttpPost]
        [Route("/number/numbers-to-n")]
        public IActionResult NumbersToN(int count = 0)
        {
            ViewBag.Count = count;
            return View();
        }
    }
}
