using hrOT.Application.EmployeeContracts;
using hrOT.Application.EmployeeContracts.Commands;
using hrOT.Application.EmployeeContracts.Commands.Add;
using hrOT.Application.EmployeeContracts.Commands.Delete;
using hrOT.Application.EmployeeContracts.Commands.Update;
using hrOT.Application.EmployeeContracts.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeeContract;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "manager")]
public class Employee_ContractController : ApiControllerBase
{
    // Xuất danh sách hợp đồng
    [HttpGet("GetListContract")]
    public async Task<IActionResult> GetListContract(Guid EmployeeID)
    {
        //
        if (EmployeeID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
                .Send(new Employee_GetListContractQuery(EmployeeID));
        //

        if (result.Count > 0)
        {
            List<string> status = new();
            List<string> salaryType = new();
            List<string> contractType = new();
            List<string> insuranceType = new();

            foreach (var itemStatus in result)
            {
                status.Add(itemStatus.GetStatusName());
                salaryType.Add(itemStatus.GetSalaryName());
                contractType.Add(itemStatus.GetContractName());
                insuranceType.Add(itemStatus.GetInsuranceName());
            }

            return Ok(result.Select(Employee_Contract => new
            {
                Employee_Contract,
                Status = status.Where(s => s == Employee_Contract.GetStatusName()).Distinct(),
                Salary_Type = salaryType.Where(s => s == Employee_Contract.GetSalaryName()).Distinct(),
                Contract_Type = contractType.Where(c => c == Employee_Contract.GetContractName()).Distinct(),
                Insurance_Type = insuranceType.Where(i => i == Employee_Contract.GetInsuranceName()).Distinct()
            }));
        }
        
        return BadRequest("Không tìm thấy bất kì hợp đồng nào");
    }

    //Thêm hợp đồng cho nhân viên
    [HttpPost("CreateContract")]
    public async Task<IActionResult> CreateContract(Guid EmployeeId, [FromForm] EmployeeContractCommandDTO employeeContractDTO)
    {
        if (EmployeeId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator.Send(new Employee_CreateContractCommand(EmployeeId, employeeContractDTO));

        return result != null
            ? Ok("Thêm thành công")
            : BadRequest("Không tìm thấy bất kì hợp đồng nào");
    }

    //Cập nhật hợp đồng cho nhân viên
    [HttpPut("UpdateContract")]
    public async Task<IActionResult> UpdateContract(Guid ContractId, Guid EmployeeId, [FromForm] EmployeeContractCommandDTO employeeContractDTO)
    {
        if (EmployeeId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator.Send(new Employee_UpdateContractCommand(ContractId, EmployeeId, employeeContractDTO));

        return Ok(result);
    }

    // Xóa hợp đồng cho nhân viên
    [HttpPut("DeleteContract")]
    public async Task<IActionResult> DeleteContract(Guid ContractId, Guid EmployeeId)
    {
        if (EmployeeId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator.Send(new Employee_DeleteContractCommand(ContractId, EmployeeId));

        return result == true
            ? Ok("Xóa thành công")
            : BadRequest("Không tìm thấy bất kì hợp đồng nào");
    }
}