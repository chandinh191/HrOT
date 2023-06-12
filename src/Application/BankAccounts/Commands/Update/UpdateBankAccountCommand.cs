using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Commands.Update;
public class UpdateBankAccountCommand : IRequest<string>
{
    public BankAccountCommandDTO _dto;
    public Guid BankId { get; set; }

    public UpdateBankAccountCommand(Guid BankID, BankAccountCommandDTO dto)
    {
        _dto = dto;
        BankId = BankID;
    }
}
public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UpdateBankAccountCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
    {
        var bank = await _context.Banks
            .Where(s => s.Id == request.BankId)
            .FirstOrDefaultAsync();
        if (bank == null) { return "Id ngân hàng không tồn tại!"; }
        if (bank.IsDeleted) { return "Ngân hàng này đã bị xóa!"; }

        var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = Guid.Parse(employeeIdCookie);

        var bankaccount = await _context.BankAccounts
            .Where(ba => ba.EmployeeId == employeeId
            && ba.BankId == request.BankId)
            .FirstOrDefaultAsync();
        if (bankaccount == null) { return "Id Nhân viên không tồn tại!"; }
        if (bankaccount.IsDeleted) { return "Ngân hàng này của nhiên viên đã bị xóa!"; }

        bankaccount.BankAccountName = request._dto.BankAccountName;
        bankaccount.BankAccountNumber = request._dto.BankAccountNumber;
        bankaccount.BankId = request.BankId;

        _context.BankAccounts.Update(bankaccount);
        await _context.SaveChangesAsync(cancellationToken);
        return "Cập nhật thành công";
    }
}
