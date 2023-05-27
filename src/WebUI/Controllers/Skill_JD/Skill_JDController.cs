using LogOT.Application.Employees.Queries;
using LogOT.Application.Employees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using hrOT.Application.Skill_JDs.Queries;

namespace WebUI.Controllers.Skill_JD;
[ApiController]
[Route("api/[controller]")]
public class Skill_JDController : ControllerBase
{
    private readonly IMediator _mediator;

    public Skill_JDController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<List<Skill_JDDTO>>> Get()
    {
        return await _mediator.Send(new GetAllSkill_JD());
    }
   
}
