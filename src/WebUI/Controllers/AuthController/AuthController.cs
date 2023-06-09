using hrOT.Application.Auth.Queries;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Common.Models;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Authentication;
using System.Threading.Tasks;
using WebUI.Models;
using static Duende.IdentityServer.Models.IdentityResources;

namespace WebUI.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityService _identityService;


        public AuthController(IMediator mediator, ILogger<AuthController> logger, UserManager<ApplicationUser> userManager, IIdentityService identityService )
        {
            _mediator = mediator;
            _logger = logger;
            _userManager = userManager;
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginWithPassword model)
        {

            string result;
            if (User.Identity.IsAuthenticated)
            {
                
                return BadRequest("Bạn đã đăng nhập.");
            }
            var usermodel = new UserModel();
            try
            {
                result = await _identityService.AuthenticateAsync(model.Username, model.Password);
                if (!String.IsNullOrEmpty(result))
                {
                    var tempUser = await _userManager.FindByNameAsync(model.Username);
                    usermodel.Username = model.Username;
                    usermodel.FullName = tempUser.Fullname;
                    usermodel.Email = tempUser.Email;
                    usermodel.userId = tempUser.Id;
                    var roles = await _userManager.GetRolesAsync(tempUser);
                    usermodel.listRoles = (List<string>)roles;
                    usermodel.token = result;
                    var jsonUser = JsonConvert.SerializeObject(usermodel);
                    return Ok(jsonUser);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Đăng nhập thất bại");


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
