using LogOT.Application.Auth.Queries;
using LogOT.Application.Common.Exceptions;
using LogOT.Domain.Entities;
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
            try
            {
                var user = await _mediator.Send(new Login { Username = model.Username, Password = model.Password });         

                // Xử lý thành công, thực hiện các hành động khác
                if (User.IsInRole("Manager"))
                {
                    return Ok("Bạn đã đăng nhập bằng tài khoản quản lý");
                }
                else if (User.IsInRole("Staff"))
                {
                    return Ok("Bạn đã đăng nhập bằng tài khoản nhân viên");
                }
                else if (User.IsInRole("Employee"))
                {
                    return Ok("Bạn đã đăng nhập bằng tài khoản người dùng");
                }

                return Ok("Đăng nhập thành công");

              
            }
            catch (NotFoundException ex)
            {
                // Xử lý khi người dùng không tồn tại
                _logger.LogError(ex, "User not found");
                return Ok("Đăng nhập tài khoản thất bại");
            }
            catch (AuthenticationException ex)
            {
                // Xử lý khi xác thực không thành công
                _logger.LogError(ex, "Authentication failed");
                return Ok("Đăng nhập mật khẩu thất bại");
            }
            catch (Exception ex)
            {
                // Xử lý khi có lỗi xảy ra
                _logger.LogError(ex, "Error during login");
                return Ok("Đăng nhập cả hai thất bại");
            }

            return Ok("Đăng nhập thất bại");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Đăng xuất người dùng

            return Ok("Logout in successfully"); // Chuyển hướng người dùng đến trang đăng nhập
        }
    }
}
