/*
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hrOT.Application.JobDescriptions;
using hrOT.Application.JobDescriptions.Queries.GetJobDescription;
using hrOT.Application.JobDescriptions.Commands.CreateJobDescription;
using hrOT.Application.JobDescriptions.Commands.UpdateJobDescription;
using hrOT.Application.JobDescriptions.Commands.DeleteJobDescription;

namespace WebUI.Controllers.JonDescriptions;


[Authorize(Policy = "manager")]
public class JobDescriptionController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<JobDescriptionDTO>>> Get()
    {
        return await Mediator.Send(new GetListJobDescriptionQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateJobDescriptionCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateJobDescriptionCommand command)
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
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeleteJobDescriptionCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }

    }
}
*/