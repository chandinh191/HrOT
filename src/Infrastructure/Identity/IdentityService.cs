using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Common.Models;
using hrOT.Domain.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace hrOT.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
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
    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

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
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = new List<Claim>();
            authClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            foreach (var item in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecrectKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

            //return await _userClaimsPrincipalFactory.CreateAsync(user) ?? throw new InvalidOperationException("Authenticated failed, please contact administrator!");
        }

        throw new InvalidOperationException("Sai mật khẩu. Vui lòng nhập lại!");
    }
}
