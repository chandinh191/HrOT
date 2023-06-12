using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Commands.Delete;
public class DeleteBankAccountCommand : IRequest<string>
{

    public Guid BankId { get; set; }

    public DeleteBankAccountCommand(Guid BankID)
    {

        BankId = BankID;
    }
}
public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteBankAccountCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
    {
        var bank = await _context.Banks
            .Where(s => s.Id == request.BankId)
            .FirstOrDefaultAsync();
        if (bank == null) { return "Id ngân hàng không tồn tại"; }
        if (bank.IsDeleted) { return "Ngân hàng này đã bị xóa"; }

        var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = Guid.Parse(employeeIdCookie);

        var bankaccount = await _context.BankAccounts
            .Where(ba => ba.EmployeeId == employeeId
            && ba.BankId == request.BankId)
            .FirstOrDefaultAsync();
        if (bankaccount == null) { return "Id nhân viên không tồn tại"; }
        if (bankaccount.IsDeleted) { return "Ngân hàng này của nhân viên đã bị xóa"; }

        bankaccount.IsDeleted = true;

        _context.BankAccounts.Update(bankaccount);
        await _context.SaveChangesAsync(cancellationToken);
        return "Xóa thành công";
    }
}
