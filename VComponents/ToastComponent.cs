using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.VComponents
{
    public class ToastComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
