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


namespace hrOT.Application.Employees.Commands.Update
{
    public record UpdateEmployee : IRequest
    {
        public Guid Id { get; set; }
        public Guid PositionId { get; set; }
        public DateTime BirthDay { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string SelectedRole { get; set; } // New property to hold the selected role
    }

    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment _environment;

        public UpdateEmployeeHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _context = context;
            this.userManager = userManager;
            _environment = environment;
        }

      

        public async Task<Unit> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            var entity = await _context.Employees
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Employee), request.Id);
            }

            entity.BankName = request.BankName;
            entity.BankAccountNumber = request.BankAccountNumber;
            entity.BankAccountName = request.BankAccountName;
            //entity.PositionId = request.PositionId;

            if (entity.ApplicationUser != null)
            {
                entity.ApplicationUser.Fullname = request.Fullname;
                entity.ApplicationUser.Address = request.Address;
                entity.ApplicationUser.BirthDay = request.BirthDay;
                entity.ApplicationUser.PhoneNumber = request.PhoneNumber;
            }

            await _context.SaveChangesAsync(cancellationToken);

            var user = await userManager.FindByIdAsync( entity.ApplicationUser.Id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, userRoles);

                await userManager.AddToRoleAsync(user, request.SelectedRole);
            }

            return Unit.Value;
        }
    }
}
