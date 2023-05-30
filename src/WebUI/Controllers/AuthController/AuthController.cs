using hrOT.Application.Auth.Queries;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Models;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Authentication;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginWithPassword model)
        {

            if (User.Identity.IsAuthenticated)
            {
                
                return BadRequest("Bạn đã đăng nhập.");
            }

            var user = await _mediator.Send(new Login { Username = model.Username, Password = model.Password });

            return Ok(user);

        }
        [HttpPost("change-password")]
        
        public async Task<IActionResult> ChangePassword([FromForm] ChangePassWord model)
        {
            try
            {
                var result = await _mediator.Send(new ChangePassWord
                {
                    Username = model.Username,
                    CurrentPassword = model.CurrentPassword,
                    NewPassword = model.NewPassword
                });

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "User not found");
                return BadRequest("Người dùng không tồn tại.");
            }
            
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPassword model)
        {
            try
            {
                var result = await _mediator.Send(new ResetPassword { Email = model.Email });

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "User not found");
                return BadRequest("Người dùng không tồn tại.");
            }
            
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Đăng xuất người dùng

            return Ok("Đăng xuát thành công"); // Chuyển hướng người dùng đến trang đăng nhập
            
        }
        private async Task<Employee> FindUserByUsername(Guid Id)
        {
            // Thực hiện logic để tìm người dùng dựa trên username
            // Ví dụ:
            var employee = await _mediator.Send(new FindUserByUsernameQuery { Id = Id });

            return employee;
        }

    }
}
