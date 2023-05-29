using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace hrOT.Application.Auth.Queries;
public class ChangePassWord : IRequest<string>
{
    public string Username { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

public class ChangePasswordHandler : IRequestHandler<ChangePassWord, string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ChangePasswordHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(ChangePassWord request, CancellationToken cancellationToken)
    {
        
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                throw new NotFoundException(nameof(ApplicationUser), request.Username);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                return("Lỗi không thể đổi mật khẩu");
            }

            return "Đổi mật khẩu thành công";
        
        
    }
}
