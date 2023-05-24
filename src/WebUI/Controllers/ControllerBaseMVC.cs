using MediatR;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace WebUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ControllerBaseMVC : Controller
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??=
        HttpContext.RequestServices.GetRequiredService<ISender>();

    private IToastNotification _toast = null!;
    protected IToastNotification Toast => _toast ??=
      HttpContext.RequestServices.GetRequiredService<IToastNotification>();
}