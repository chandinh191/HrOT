using hrOT.Application.EmployeeContracts.Queries;
using hrOT.Domain.Enums;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace WebUI.Controllers.EmployeeContract;

[Route("api/[controller]")]
[ApiController]
public class Employee_ContractController : ApiControllerBase
{
    // Xuất danh sách hợp đồng
    [HttpGet("GetListHopDong")]
    public async Task<IActionResult> GetListHopDong(Guid EmployeeID)
    {
        if (EmployeeID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_GetListContractQuery(EmployeeID));
        if (result != null)
        {
            var status = "";
            foreach (var itemStatus in result)
            {
                switch (itemStatus.Status)
                {
                    case EmployeeContractStatus.Effective:
                        status = EmployeeContractStatus.Effective.GetDisplayName();
                        break;

                    case EmployeeContractStatus.Expired:
                        status = EmployeeContractStatus.Expired.GetDisplayName();
                        break;

                    case EmployeeContractStatus.Renewed:
                        status = EmployeeContractStatus.Renewed.GetDisplayName();
                        break;

                    case EmployeeContractStatus.Cancelled:
                        status = EmployeeContractStatus.Cancelled.GetDisplayName();
                        break;
                }
            }
            return Ok(result.Select(r => new { r, status }));
        }
        return BadRequest($"Không tìm thấy bất kì kinh nghiệm bản thân nào của EmployeeID: {EmployeeID}");
    }
}