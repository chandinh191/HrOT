using System;
using System.Threading;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using AutoMapper.Internal;


namespace hrOT.Application.Employees.Commands.Update
{
    public record UpdateEmployee : IRequest<string>
    {
       public Guid EmployeeId { get; set; }
        public Guid PositionId { get; set; }
        public string? CitizenIdentificationNumber { get; set; }
        public DateTime? CreatedDateCIN { get; set; }
        public string? PlaceForCIN { get; set; }
        public DateTime BirthDay { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        //Địa chỉ
        public string? District { get; set; }
        public string? Province { get; set; }
        public string SelectedRole { get; set; } 
    }

    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateEmployeeHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.userManager = userManager;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

      

        public async Task<string> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            //Truy vấn lấy EmployeeId
            var entity = await _context.Employees
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);
            //Kiểm tra EmployeeId có bị xóa chưa
            if (entity == null)
            {

                throw new NotFoundException(nameof(Employee), request.EmployeeId);
            } else if (entity.IsDeleted == true)
            {
                return "Nhân viên này đã bị xóa!";

            }
            entity.Id = request.EmployeeId;
            entity.CitizenIdentificationNumber = request.CitizenIdentificationNumber;
            entity.CreatedDateCIN = request.CreatedDateCIN;
            entity.PlaceForCIN = request.PlaceForCIN;
            entity.District = request.District;
            entity.Province = request.Province;
            entity.Address = request.Address;
            entity.PositionId = request.PositionId;
            //Kiểm tra ApplicationUser có null ko
            if (entity.ApplicationUser != null)
            {
                entity.ApplicationUser.Fullname = request.Fullname;
               
                entity.ApplicationUser.BirthDay = request.BirthDay;
                entity.ApplicationUser.PhoneNumber = request.PhoneNumber;
            }

            await _context.SaveChangesAsync(cancellationToken);
            // tìm kiếm người dùng theo Id của ApplicationUser
            var user = await userManager.FindByIdAsync( entity.ApplicationUser.Id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                // Xóa role hiện tại
                await userManager.RemoveFromRolesAsync(user, userRoles);
                //Add role mới vào
                await userManager.AddToRoleAsync(user, request.SelectedRole);
            }

            return "Cập nhật thành công";
        }
    }
}
