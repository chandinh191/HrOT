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
using System.Text;
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
                

            }catch(Exception ex)
            {
                throw new ("Thông tin người dùng không hợp lệ");
            }


            throw new("Thông tin người dùng không hợp lệ");
        }

    }
}
