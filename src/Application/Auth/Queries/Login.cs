﻿using hrOT.Application.Common.Exceptions;
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
    public class Login : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginHandler : IRequestHandler<Login, bool>
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

        public async Task<bool> Handle(Login request, CancellationToken cancellationToken)
        {
            try
            {
                bool result = false;
                //Kiểm tra UserName
                var user = await _userManager.FindByNameAsync(request.Username.Trim());
                if (user == null)
                {
                    throw new NotFoundException(nameof(ApplicationUser), request.Username);
                }
                //Kiểm tra Pass
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password.Trim());

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    var httpContext = _httpContextAccessor.HttpContext;
                    var response = httpContext.Response;
                    var userManager = _userManager;

                    var roles = await userManager.GetRolesAsync(user);
                    //Tạo List Claim để lưu trữ các thông tin trong ApplicationUser
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("Id", user.Id)
           
                    };
                    // Thêm FullName vào cookie
                    response.Cookies.Append("FullName", user.Fullname);

                    //Lấy role 
                    //Sử dụng chính sách (policies) cho xác thực trong identity để phân quyền..
                    // Nó nằm ở Infrastructure/ConfigureServieces
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                        //Đưa role vào cookie
                        response.Cookies.Append("Role", role);
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    //Lấy id của Employee
                    var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ApplicationUserId == user.Id);

                    if (employee != null)
                    {
                        var employeeId = employee.Id;
                       
                        response.Cookies.Append("EmployeeId", employeeId.ToString());
                    }
                    //Thông báo role khi đăng nhập

                    result= true;
                        return result;
                }

            }catch(Exception ex)
            {
                throw new ("Thông tin người dùng không hợp lệ");
            }


            throw new("Thông tin người dùng không hợp lệ");
        }

    }
}
