using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Commands.Create;
public class CreateBankAccountCommand : IRequest<string>
{
    public Guid BankId { get; set; }
    public BankAccountCommandDTO BankAccountDTO { get; set; }

    public CreateBankAccountCommand(Guid BankID, BankAccountCommandDTO bankAccountDTO)
    {
        BankId = BankID;
        BankAccountDTO = bankAccountDTO;
    }
}
public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateBankAccountCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var bank = await _context.Banks
            .Where(s => s.Id == request.BankId)
            .FirstOrDefaultAsync();
        if (bank == null) { return "Id ngân hàng không tồn tại!"; }
        if (bank.IsDeleted) { return "Ngân hàng này đã bị xóa!"; }

        //var employee = await _context.Employees
        //    .Where(e => e.Id == request.EmployeeId)
        //    .FirstOrDefaultAsync();
        //if (employee == null) { return "Id nhân viên không tồn tại!"; }
        //if (employee.IsDeleted) { return "Nhân viên này đã bị xóa!"; }

        var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = Guid.Parse(employeeIdCookie);

        var bankaccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            BankAccountName = request.BankAccountDTO.BankAccountName,
            BankAccountNumber = request.BankAccountDTO.BankAccountNumber,
            EmployeeId = employeeId,
            BankId = request.BankId
        };

        await _context.BankAccounts.AddAsync(bankaccount);
        await _context.SaveChangesAsync(cancellationToken);
        return "Thêm thành công";
    }
}
