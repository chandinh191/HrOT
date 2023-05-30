using hrOT.Application.Common.Exceptions;
using hrOT.Application.Employees;
using hrOT.Application.Employees.Commands.Create;
using hrOT.Application.Employees.Commands.Delete;
using hrOT.Application.Employees.Commands.Update;
using hrOT.Application.Employees.Queries;
using hrOT.WebUI.Controllers;
using LogOT.Application.Employees;
using LogOT.Application.Employees.Commands.Create;
using LogOT.Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "manager")]
        public async Task<ActionResult<List<EmployeeDTO>>> Get()
        {
            //var employeeIdCookie = Request.Cookies["EmployeeId"];
            return await _mediator.Send(new GetAllEmployeeQuery());

        }

        [HttpPost("create")]
        //[Authorize(Policy = "manager")]
        public async Task<IActionResult> CreateEmployee([FromForm] CreateEmployee createModel)
        {

            if (ModelState.IsValid && createModel != null)
            {
                var entityId = await _mediator.Send(createModel);

                return Ok("Thêm thành công");
            }

            return BadRequest("Thêm thất bại");

        }

        [HttpPut]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> Edit( [FromForm] UpdateEmployee command)
        {
           
            try
            {
                

                await _mediator.Send(command);
                return Ok("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Cập nhật thất bại");
            }
        }

        [HttpPut("[action]")]
        [Authorize(Policy = "manager")]
        public async Task<IActionResult> Delete(Guid id, [FromForm] DeleteEmployee command)

        {
            if (id != command.Id)
            {
                return Ok();
            }

            try
            {
                await _mediator.Send(command);

                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Xóa thất bại");
            }
        }

        [HttpPost("CreateEx")]
        [Authorize(Policy = "manager")]
        public async Task<IActionResult> CreateEx(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Kiểm tra kiểu tệp tin
                if (!IsExcelFile(file))
                {
                    return BadRequest("Chỉ cho phép sử dụng file Excel");
                }

                var filePath = Path.GetTempFileName(); // Tạo một tệp tạm để lưu trữ tệp Excel
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream); // Lưu tệp Excel vào tệp tạm
                }

                var command = new CreateEmployeeEx
                {
                    FilePath = filePath
                };

                await _mediator.Send(command);

                return Ok("Thêm thành công");
            }

            return BadRequest("Thêm thất bại");
        }

        private bool IsExcelFile(IFormFile file)
        {
            // Kiểm tra phần mở rộng của tệp tin có phải là .xls hoặc .xlsx không
            var allowedExtensions = new[] { ".xls", ".xlsx" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }


        [HttpGet("GetEmployeeById")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                var query = new Employee_GetEmployeeQuery {  };
                var employeeVm = await _mediator.Send(query);

                if (employeeVm == null)
                {
                    return BadRequest("Không tìm thấy nhân viên");
                }

                return Ok(employeeVm);
            }
            catch (NotFoundException)
            {
                return BadRequest("Không tìm thấy nhân viên");
            }
        }

        [HttpPost("uploadCv")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> UploadCV( IFormFile cvFile)
        {
            try
            {
                if (cvFile == null || cvFile.Length == 0)
                {
                    return BadRequest("Không tìm thấy file");
                }

                var command = new Employee_EmployeeUploadCVCommand
                {
                   
                    CVFile = cvFile
                };

                await _mediator.Send(command);

                return Ok("Cập nhật CV thành công");
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return BadRequest("Lỗi cập nhật CV");
            }
        }

        /*[HttpGet("GetEmployeeByMatchingJobSkill")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> GetEmployeeByMatchingJobSkill(string SkillName)
        {
            if (SkillName == null)
            {
                return BadRequest("Vui lòng nhập tên kĩ năng !");
            }

            var result = await Mediator.Send(new Employee_GetByMatchingJobDescriptionSkillQuery(SkillName));

            return (result != null )
                ? Ok(result)
                : BadRequest("Không tìm thấy nhân viên có kĩ năng phù hợp với công việc.");
        }*/

        [HttpPost("uploadImage")]
        [Authorize(Policy = "ManagerOrStaff")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Không tìm thấy hình ảnh");
                }
                if (!IsImageFile(imageFile))
                {
                    return BadRequest("Bạn phải sử dụng file hình ảnh");
                }
                var command = new UpLoadImage
                {
                    File = imageFile
                };

                await _mediator.Send(command);

                return Ok("Cập nhật ảnh đại diện thành công");

            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return BadRequest("Lỗi cập nhật hình ảnh");
            }
        }
        private bool IsImageFile(IFormFile file)
        {
            // Kiểm tra phần mở rộng của tệp tin có phải là hình ảnh không
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }
        /*[HttpPost("{id}/uploadIdentityImage")]
        [Authorize(Policy = "employee")]
        public async Task<IActionResult> UploadIdentityImage(Guid id, IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Không tìm thấy hình ảnh");
                }

                var command = new UploadIdentityImage
                {
                    Id = id,
                    File = imageFile
                };

                await _mediator.Send(command);

                return Ok("Cập nhật ảnh căn cước thành công");
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return BadRequest("Lỗi cập nhật hình ảnh");
            }
        }
        [HttpPost("{id}/uploadDiploma")]
        [Authorize(Policy = "employee")]
        public async Task<IActionResult> UploadDiploma(Guid id, IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("Không tìm thấy hình ảnh");
                }

                var command = new UpLoadDiploma
                {
                    Id = id,
                    File = imageFile
                };

                await _mediator.Send(command);

                return Ok("Cập nhật ảnh bằng cấp thành công");
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return BadRequest("Lỗi cập nhật hình ảnh");
            }
        }*/
    }
}