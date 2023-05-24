using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
public class HomeController : ControllerBaseMVC
{
    public IActionResult Index()
    {
        return View();
    }
}
