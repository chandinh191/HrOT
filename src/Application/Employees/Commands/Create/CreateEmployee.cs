using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;


namespace hrOT.Application.Employees.Commands.Create
{
    public record CreateEmployee : IRequest<string>
    {
        // Thẻ căn cước
        public Guid PositionId { get; set; }
        public string? CitizenIdentificationNumber { get; set; }
        public DateTime? CreatedDateCIN { get; set; }
        public string? PlaceForCIN { get; set; }
        // Ngân Hàng
        public DateTime BirthDay { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string FullName { get; set; }
        public string UserName { get;  set; }
       
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Password { get; set; }
        public string SelectedRole { get; set; }
        //Địa chỉ
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployee, string>
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationDbContext _context;
        public CreateEmployeeCommandHandler(IApplicationDbContext context, IIdentityService identityService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _identityService = identityService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<string> Handle(CreateEmployee request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,              
                Email = request.Email,
                Fullname = request.FullName,
                PhoneNumber = request.PhoneNumber,
                BirthDay = request.BirthDay,
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, request.SelectedRole);
                await _signInManager.SignInAsync(user, isPersistent: false);
               
            }
            else
            {
                
            }

            var entity = new Employee
            {   PositionId = request.PositionId,
                ApplicationUserId = user.Id,
                CitizenIdentificationNumber = request.CitizenIdentificationNumber,
                CreatedDateCIN = request.CreatedDateCIN,
                PlaceForCIN = request.PlaceForCIN,
                Address = request.Address,
                District = request.District,
                Province = request.Province,              
                BankName = request.BankName,
                BankAccountNumber = request.BankAccountNumber,
                BankAccountName = request.BankAccountName,
            };

            _context.Employees.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.ApplicationUserId;
        }
    }

}
