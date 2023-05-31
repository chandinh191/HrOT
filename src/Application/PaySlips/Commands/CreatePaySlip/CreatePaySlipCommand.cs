using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.SalaryCalculators;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.PaySlips.Commands.CreatePaySlip;
public record CreatePaySlipCommand() : IRequest<string>
{
    public Guid EmployeeId { get; set; }
    public DateTime ToDate { get; set; }
}

public class CreatePaySlipCommandHandler : IRequestHandler<CreatePaySlipCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreatePaySlipCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreatePaySlipCommand request, CancellationToken cancellationToken)
    {
        //khai báo
        DateTime? FromDate;
        double? Salary = 0;
        double? Gross = 0;
        double? Net = 0;
        double? Salary_1Hour = 0;
        int? Standard_Work_Hours = 0;
        double? Actual_Work_Hours = 0;
        double? Ot_Hours = 0;
        double? Leave_Hours = 0;
        double? Bonus = 0;
        double? Deduction = 0;
        double? Exchange_Salary = 0;
        double? BHXH_Emp = 0;
        double? BHYT_Emp = 0;
        double? BHTN_Emp = 0;
        double? BHXH_Cmp = 0;
        double? BHYT_Cmp = 0;
        double? BHTN_Cmp = 0;
        double? TNTT = 0;
        double? TNCT = 0;
        double? TTNCN = 0;
        double? SalaryFinal = 0;
        double? Company_Paid = 0;
        var NumberOfDependencies = await _context.Families
            .Where(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false)
            .ToListAsync(cancellationToken);
        var EmployeeContract = await _context.EmployeeContracts
            .Where(x => x.EmployeeId == request.EmployeeId && x.Status == EmployeeContractStatus.Effective)
            .SingleOrDefaultAsync(cancellationToken);
        if( EmployeeContract == null)
        {
            return "Không tìm thấy hợp đồng của nhân viên";
        }
        var Last_PaySlip = await _context.PaySlips
            .Where(x => x.EmployeeContractId == EmployeeContract.Id)
            .SingleOrDefaultAsync(cancellationToken);
        //Nếu chưa có Payslip nào thì FromDate == StartDate trong EmployeeContract
        if (Last_PaySlip == null)
        {   
            if (EmployeeContract.StartDate >= request.ToDate)
            {
                return $"{request.ToDate.ToString("dd/MM/yyyy")} là ngày bắt đầu của hợp đồng chưa thể tính lương";
            }
            else
            {
                FromDate = EmployeeContract.StartDate;
            }
        }
        else
        {
            if(Last_PaySlip.Paid_date == request.ToDate)
            {
                return $"Hợp đồng này đã được tính lương ngày: {request.ToDate.ToString("dd/MM/yyyy")}";
            }
            else
            {
                FromDate = Last_PaySlip.Paid_date;
            }        
        }

        //lấy ra số giờ làm việc tiêu chuẩn của tháng đó
        var AnnualWorkingDays = await _context.AnnualWorkingDays
        .Where(x => x.Day.Month == request.ToDate.Month && x.TypeDate == TypeDate.Weekday)
        .ToListAsync(cancellationToken);
        if(AnnualWorkingDays.Count == 0) 
        {
            return "Vui lòng cập nhật danh sách ngày làm việc hàng năm";
        }
        Standard_Work_Hours = AnnualWorkingDays.Count * 8;

        var List_TaxInCome = await _context.TaxInComes
            .Where(t => t.IsDeleted == false)
            .OrderBy(t => t.Muc_chiu_thue)
            .ToListAsync(cancellationToken);
        var List_Exchange = await _context.Exchanges
            .Where(t => t.IsDeleted == false)
            .OrderBy(e => e.Muc_Quy_Doi)
            .ToListAsync(cancellationToken);

        Salary_1Hour = EmployeeContract.Salary / Standard_Work_Hours;

        //tính tiền lương cơ bản cho nhân viên
        var TimeAttendanceLog = await _context.TimeAttendanceLogs
            .Where(x => x.EmployeeId == EmployeeContract.EmployeeId && x.StartTime >= FromDate && x.StartTime < request.ToDate)
            .ToListAsync(cancellationToken);
        if(TimeAttendanceLog.Count == 0)
        {
            return "Không tìm thấy lịch sử chấm công của nhân viên";
        }
        foreach (var Log in TimeAttendanceLog)
        {
            if(Log.Ducation == 0)
            {
                return "Vui lòng tính chấm công cho nhân viên trước khi tính lương";
            }
            if(Log.Ducation > 8)
            {
                Actual_Work_Hours += 8;
            }
            else
            {
                Actual_Work_Hours += Log.Ducation;
            }         
        }
        //tính lương trong tháng đó của nhân viên ( đã trừ lương ngày nghỉ chưa tính lương tăng ca)
        Salary = Actual_Work_Hours * Salary_1Hour;

        var OvertimeLog = await _context.OvertimeLogs
            .Where(x => x.EmployeeId == EmployeeContract.EmployeeId && x.Status == OvertimeLogStatus.Approved && x.IsDeleted == false && x.StartDate >= FromDate && x.StartDate < request.ToDate)
            .ToListAsync(cancellationToken);
        var LeaveLog = await _context.LeaveLogs
            .Where(x => x.EmployeeId == EmployeeContract.EmployeeId && x.Status == LeaveLogStatus.Approved && x.IsDeleted == false && x.StartDate >= FromDate && x.StartDate < request.ToDate)
            .ToListAsync(cancellationToken);
        Ot_Hours = OvertimeLog.Sum(x => x.TotalHours);
        foreach (var overtimeLog in OvertimeLog)
        {
            Bonus += overtimeLog.TotalHours * overtimeLog.Coefficients * Salary_1Hour;
        }
        Leave_Hours = LeaveLog.Sum(x => x.LeaveHours);
        Deduction = Leave_Hours * Salary_1Hour;

        int n = List_TaxInCome.Count();
        int m = List_Exchange.Count();

        double?[] DetailTaxInComes = new double?[n];
        for (int i = 0; i < n; i++)
        {
            DetailTaxInComes[i] = 0;
        }
        double?[] DetailTaxInCome_Max = new double?[n];
        for (int i = 0; i < n - 1; i++)
        {
            if (i == 0)
            {
                DetailTaxInCome_Max[i] = List_TaxInCome[i].Muc_chiu_thue * List_TaxInCome[i].Thue_suat;
            }
            else
            {
                DetailTaxInCome_Max[i] = (List_TaxInCome[i].Muc_chiu_thue - List_TaxInCome[i - 1].Muc_chiu_thue) * List_TaxInCome[i].Thue_suat;
            }
        }

        var Total_Allowance = await _context.Allowances
            .Where(x => x.EmployeeContractId == EmployeeContract.Id && x.IsDeleted == false)
            .SumAsync(x => x.Amount, cancellationToken);    

        //nếu là lương NET
        if (EmployeeContract.SalaryType == SalaryType.Net)
        {
            Gross = Salary;
            //tính các loại bảo hiểm nhân viên phải trả 
            //tính trên lương
            if (EmployeeContract.InsuranceType == InsuranceType.Official)
            {
                BHXH_Emp = Gross * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = Gross * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = Gross * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }
            //tính trên số khác
            else
            {
                BHXH_Emp = EmployeeContract.CustomSalary * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = EmployeeContract.CustomSalary * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = EmployeeContract.CustomSalary * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }

            //tính thu nhập trước thuế
            TNTT = Gross - BHXH_Emp - BHYT_Emp - BHTN_Emp;
            //tính thu nhập chịu thuế
            TNCT = (double)(TNTT - 11000000 - NumberOfDependencies.Count * 4400000);
            // nếu thu nhập chịu thuế > 0 thì tính thuế thu nhập cá nhân
            if (TNCT > 0)
            {
                // Nếu TNCT nhỏ hơn mức chịu thuế đầu tiên
                if (TNCT <= List_TaxInCome[0].Muc_chiu_thue)
                {
                    // Tính thuế cho mức thu nhập chịu thuế là TNCT
                    TTNCN = TNCT * List_TaxInCome[0].Thue_suat;
                    // Thêm giá trị TTNCN vào danh sách TTNCN_Detail
                    DetailTaxInComes[0] = TTNCN;
                }
                else
                {
                    for (int i = 1; i < n; i++)
                    {
                        if (TNCT <= List_TaxInCome[i].Muc_chiu_thue)
                        {
                            TTNCN = (TNCT - List_TaxInCome[i - 1].Muc_chiu_thue) * List_TaxInCome[i].Thue_suat;
                            DetailTaxInComes[i] = DetailTaxInComes[i] = Math.Ceiling(TTNCN.Value);
                            for (int j = 0; j < i; j++)
                            {
                                TTNCN += DetailTaxInCome_Max[j];
                                DetailTaxInComes[j] = DetailTaxInCome_Max[j];
                            }
                            break;
                        }

                    }
                }
            }

            //tính lương cuối
            Net = TNTT - TTNCN + Bonus + Total_Allowance;
            SalaryFinal = Net;

            //tính các loại bảo hiểm công ty phải trả 
            BHXH_Cmp = Gross * 0.175;
            if (BHXH_Cmp > 5215000)
            {
                BHXH_Cmp = 5215000;
            }
            BHYT_Cmp = Gross * 0.03;
            if (BHYT_Cmp > 894000)
            {
                BHYT_Cmp = 894000;
            }
            BHTN_Cmp = Gross * 0.01;
            if (BHTN_Cmp > 884000)
            {
                BHTN_Cmp = 884000;
            }
            Company_Paid = SalaryFinal + BHXH_Cmp + BHYT_Cmp + BHTN_Cmp + Total_Allowance;
        }

        //nếu là lương GROSS
        else
        {
            //tính lương Net = thu nhập 
            Net = Salary;
            //tính lương quy đổi
            Exchange_Salary = (double)(Net - 11000000 - NumberOfDependencies.Count * 4400000);

            //sử dụng bảng quy đổi để tính thu nhập chịu thuế
            if (Exchange_Salary > 0)
            {
                // Nếu thu nhập quy đổi nhỏ hơn mức quy đổi đầu tiên
                if (Exchange_Salary <= List_Exchange[0].Muc_Quy_Doi)
                {
                    TNCT = (Exchange_Salary - List_Exchange[0].Giam_Tru) / List_Exchange[0].Thue_Suat;
                }
                else
                {
                    for (int i = 1; i < m; i++)
                    {
                        if (Exchange_Salary <= List_Exchange[i].Muc_Quy_Doi)
                        {
                            TNCT = (Exchange_Salary - List_Exchange[i].Giam_Tru) / List_Exchange[i].Thue_Suat;
                            break;
                        }

                    }

                }
            }

            // nếu thu nhập chịu thuế > 0 thì tính thuế thu nhập cá nhân
            if (TNCT > 0)
            {
                // Nếu TNCT nhỏ hơn mức chịu thuế đầu tiên
                if (TNCT <= List_TaxInCome[0].Muc_chiu_thue)
                {
                    // Tính thuế cho mức thu nhập chịu thuế là TNCT
                    TTNCN = TNCT * List_TaxInCome[0].Thue_suat;
                    // Thêm giá trị TTNCN vào danh sách TTNCN_Detail
                    DetailTaxInComes[0] = TTNCN;
                }
                else
                {
                    for (int i = 1; i < n; i++)
                    {
                        if (TNCT <= List_TaxInCome[i].Muc_chiu_thue)
                        {
                            TTNCN = (TNCT - List_TaxInCome[i - 1].Muc_chiu_thue) * List_TaxInCome[i].Thue_suat;
                            DetailTaxInComes[i] = DetailTaxInComes[i] = Math.Ceiling(TTNCN.Value);
                            for (int j = 0; j < i; j++)
                            {
                                TTNCN += DetailTaxInCome_Max[j];
                                DetailTaxInComes[j] = DetailTaxInCome_Max[j];
                            }
                            break;
                        }

                    }
                }
            }
            //tính thu nhập trước thuế bằng thu nhập + thuế thu nhập cá nhân
            TNTT = Net + TTNCN;
            //tính các loại bảo hiểm nhân viên phải trả 
            //tính trên lương
            if (EmployeeContract.InsuranceType == InsuranceType.Official)
            {
                //chia cho 0.895 để ra lương gross, từ lương gross tính ra các BH
                Gross = TNTT / 0.895;
                BHXH_Emp = Gross * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = Gross * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = Gross * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }
            //tính trên số khác
            else
            {
                //tính các loại bảo hiểm nhân viên phải trả 
                BHXH_Emp = EmployeeContract.CustomSalary * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = EmployeeContract.CustomSalary * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = EmployeeContract.CustomSalary * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }
            //tính lương cuối
            Gross = TNTT + BHXH_Emp + BHYT_Emp + BHTN_Emp + Bonus + Total_Allowance;
            SalaryFinal = Gross;

            //tính các loại bảo hiểm công ty phải trả 
            BHXH_Cmp = Gross * 0.175;
            if (BHXH_Cmp > 5215000)
            {
                BHXH_Cmp = 5215000;
            }
            BHYT_Cmp = Gross * 0.03;
            if (BHYT_Cmp > 894000)
            {
                BHYT_Cmp = 894000;
            }
            BHTN_Cmp = Gross * 0.01;
            if (BHTN_Cmp > 884000)
            {
                BHTN_Cmp = 884000;
            }
            Company_Paid = SalaryFinal + BHXH_Cmp + BHYT_Cmp + BHTN_Cmp + Total_Allowance;
        }

        //tạo payslip
        var payslip = new PaySlip
        {
            EmployeeContractId = EmployeeContract.Id,
            Standard_Work_Hours = Standard_Work_Hours,
            Actual_Work_Hours = Actual_Work_Hours,
            Ot_Hours = (int)Ot_Hours,
            Leave_Hours = (int)Leave_Hours,
            Salary = Math.Ceiling(Salary.Value),
            BHXH_Emp = Math.Ceiling(BHXH_Emp.Value),
            BHYT_Emp = Math.Ceiling(BHYT_Emp.Value),
            BHTN_Emp = Math.Ceiling(BHTN_Emp.Value),
            BHXH_Comp = Math.Ceiling(BHXH_Cmp.Value),
            BHYT_Comp = Math.Ceiling(BHYT_Cmp.Value),
            BHTN_Comp = Math.Ceiling(BHTN_Cmp.Value),
            Tax_In_Come = Math.Ceiling(TTNCN.Value),
            Bonus = Math.Ceiling(Bonus.Value),
            Deduction = Math.Ceiling(Deduction.Value),
            Total_Allowance = Math.Ceiling(Total_Allowance),
            Final_Salary = Math.Ceiling(SalaryFinal.Value),
            Company_Paid = Math.Ceiling(Company_Paid.Value),
            Paid_date = request.ToDate,
            CreatedBy = "Admin",
            LastModified = DateTime.Now,
            LastModifiedBy = "Admin"
        };
        _context.PaySlips.Add(payslip);

        //Lưu Detail TaxInCome
        for (int i = 0; i < DetailTaxInComes.Length; i++)
        {
            var detail = new DetailTaxIncome
            {
                PaySlipId = payslip.Id,
                Level = i + 1,
                Payment = DetailTaxInComes[i],
                CreatedBy = "Admin",
                LastModified = DateTime.Now,
                LastModifiedBy = "Admin"
            };
            _context.DetailTaxIncomes.Add(detail);
        }

        //xóa hết overtimelog
        foreach (var item in OvertimeLog)
        {
            if (item.IsDeleted == false)
            {
                item.IsDeleted = true;
                item.LastModified = DateTime.Now;
                item.LastModifiedBy = "Admin";
            }
        }
        //xóa hết Leavelog
        foreach (var item in LeaveLog)
        {
            if (item.IsDeleted == false)
            {
                item.IsDeleted = true;
                item.LastModified = DateTime.Now;
                item.LastModifiedBy = "Admin";
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return "Tính lương thành công";
    }
}