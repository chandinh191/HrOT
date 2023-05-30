/*using hrOT.Application.Allowances.Command.Create;
using hrOT.Application.Allowances.Command.Delete;
using hrOT.Application.Allowances.Command.Update;
using hrOT.Application.Allowances.Queries;
using hrOT.Application.InterviewProcesses.Commands.Create;
using hrOT.Application.InterviewProcesses.Commands.Delete;
using hrOT.Application.InterviewProcesses.Commands.Update;
using hrOT.Application.InterviewProcesses.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.InterviewProcesses;
public class InterviewProcessController : ApiControllerBase
{
    [HttpGet("{EmployeeId}")]
    public async Task<ActionResult<List<InterviewProcessDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        return await Mediator.Send(new GetListInterviewProcessByEmployeeIdQuery(EmployeeId));
    }
    [HttpGet("{JobId}")]
    public async Task<ActionResult<List<InterviewProcessDto>>> GetListByJobId(Guid JobId)
    {
        return await Mediator.Send(new GetListInterviewProcessByJobIdQuery(JobId));
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateInterviewProcessCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateInterviewProcessCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
        try
        {
            await Mediator.Send(command);
            return Ok("Cập nhật thành công");

        }
        catch (Exception ex)
        {
            return BadRequest("Cập nhật thất bại");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteInterviewProcessCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
        try
        {
            await Mediator.Send(command);
            return Ok("Xóa thành công");

        }
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }
    }
}
*/