using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace hrOT.Application.Auth.Queries
{
    public class Login : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginHandler : IRequestHandler<Login, string>
    {

        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _context;
        public LoginHandler(IIdentityService identityService, UserManager<ApplicationUser> userManager,IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _identityService = identityService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Handle(Login request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AuthenticateAsync(request.Username.Trim(), request.Password.Trim());

            if (result.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                    throw new NotFoundException(nameof(ApplicationUser), request.Username);

                var httpContext = _httpContextAccessor.HttpContext;
                var response = httpContext.Response;
                var userManager = _userManager;

                var roles = await userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Id", user.Id),
                };

                response.Cookies.Append("FullName", user.Fullname);
                response.Cookies.Append("Image", user.Image);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                /*var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ApplicationUserId == user.Id);
                var employeeId = employee.Id;
                response.Cookies.Append("EmployeeId", employeeId.ToString());*/

                if (await userManager.IsInRoleAsync(user, "Manager"))
                {


                    return "Đăng nhập thành công với quyền quản lý";
                }
                else if (await userManager.IsInRoleAsync(user, "Staff"))
                {


                    return "Đăng nhập thành công với quyền nhân viên";
                }
                else if (await userManager.IsInRoleAsync(user, "Employee"))
                {


                    return "Đăng nhập thành công với quyền người dùng";
                }
            }

            throw new AuthenticationException("Invalid user details");
        }
    }
}
