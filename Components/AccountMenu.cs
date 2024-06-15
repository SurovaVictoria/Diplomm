using Microsoft.AspNetCore.Mvc;
namespace Diplomm.Components
{
    public class AccountMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(HttpContext.User);
        }
    }
}