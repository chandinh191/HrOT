using System.Security.Claims;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Common.Models;
using hrOT.Domain.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, 
        SignInManager<ApplicationUser> signInManager,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _signInManager = signInManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
    public async Task<ClaimsPrincipal> AuthenticateAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        var a = user;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tên đăng nhập hoặc địa chỉ email '{username}'");
            }
        }
        if (user.LockoutEnd != null && user.LockoutEnd.Value > DateTime.Now)
        {
            throw new KeyNotFoundException($"Tài khoản này hiện tại đang bị khóa. Vui lòng liên hệ quản trị viên để được hỗ trợ");
        }
        /*if (user.EmailConfirmed == false)
        {
            throw new KeyNotFoundException($"Email của tài khoản này chưa được xác nhận. Vui lòng nhấn quên mật khẩu!");

        }*/

        //sign in  
        var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (signInResult.Succeeded)
        {
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var principal = await _signInManager.CreateUserPrincipalAsync(user);
            //var token = new JwtSecurityToken(issuer: _configuration["JWT:ValidIssuer"]
            //    , audience: _configuration["JWT:ValidAudience"],
            //    claims: principal.Claims,
            //    expires: DateTime.Now.AddHours(2),
            //    signingCredentials: creds);

            //return new JwtSecurityTokenHandler().WriteToken(token);

            return await _userClaimsPrincipalFactory.CreateAsync(user) ?? throw new InvalidOperationException("Authenticated failed, please contact administrator!");
        }

        throw new InvalidOperationException("Sai mật khẩu. Vui lòng nhập lại!");
    }
}
