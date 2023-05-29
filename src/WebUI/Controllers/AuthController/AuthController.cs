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
            
                var user = await _mediator.Send(new Login { Username = model.Username, Password = model.Password });

                // Xử lý thành công, thực hiện các hành động khác


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
                return Ok("Người dùng không tồn tại.");
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
                return Ok("Người dùng không tồn tại.");
            }
            
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Đăng xuất người dùng

            return Ok("Đăng xuát thành công"); // Chuyển hướng người dùng đến trang đăng nhập
            
        }
    }
}
