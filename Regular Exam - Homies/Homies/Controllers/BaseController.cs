using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Homies.Controllers
{
    public class BaseController : Controller
    {
        [Authorize]
        protected string GetUserId()
        {
            string id = string.Empty;

            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return id;
        }
    }
}
