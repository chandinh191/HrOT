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
public record CreatePaySlipCommand(Guid EmployeeId) : IRequest<Guid>;

public class CreatePaySlipCommandHandler : IRequestHandler<CreatePaySlipCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreatePaySlipCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreatePaySlipCommand request, CancellationToken cancellationToken)
    {
        //khai báo
        double? Gross = 0;
        double? Net = 0;
        double? Salary_1Hour = 0;
        int? Standard_Work_Hours = 240;
        int? Actual_Work_Hours;
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

        var EmployeeContract = await _context.EmployeeContracts
            .Where(x => x.EmployeeId == request.EmployeeId && x.Status == EmployeeContractStatus.Effective)
            .SingleOrDefaultAsync(cancellationToken);

        var List_TaxInCome = await _context.TaxInComes
            .OrderBy(t => t.Muc_chiu_thue)
            .ToListAsync(cancellationToken);
        var List_Exchange = await _context.Exchanges
            .OrderBy(e => e.Muc_Quy_Doi)
            .ToListAsync(cancellationToken);

        Salary_1Hour = EmployeeContract.Salary / Standard_Work_Hours;
        var OvertimeLog = await _context.OvertimeLogs
            .Where(x => x.Status == OvertimeLogStatus.Approved && x.IsDeleted == false)
            .ToListAsync(cancellationToken);
        var LeaveLog = await _context.LeaveLogs
            .Where(x => x.Status == LeaveLogStatus.Approved && x.IsDeleted == false)
            .ToListAsync(cancellationToken);
        Ot_Hours = OvertimeLog.Sum(x => x.TotalHours);
        Leave_Hours = LeaveLog.Sum(x => x.LeaveHours);
        Bonus = Ot_Hours * Salary_1Hour * 1.5;
        Deduction = Leave_Hours * Salary_1Hour;

        int n = List_TaxInCome.Count();
        int m = List_Exchange.Count();

        double?[] DetailTaxInComes = new double?[n];
        for (int i = 1; i < n; i++)
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
            .Where(x => x.EmployeeContractId == EmployeeContract.Id)
            .SumAsync(x => x.Amount, cancellationToken);    

        //nếu là lương NET
        if (EmployeeContract.SalaryType == SalaryType.Net)
        {
            Gross = EmployeeContract.Salary;
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
            TNTT = Gross - BHXH_Emp - BHYT_Emp - BHTN_Emp - Deduction;
            //tính thu nhập chịu thuế
            TNCT = (double)(TNTT - 11000000 - EmployeeContract.Number_Of_Dependents * 4400000);
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
            Company_Paid = SalaryFinal + BHXH_Cmp + BHYT_Cmp + BHTN_Cmp;
        }

        //nếu là lương GROSS
        else
        {
            //tính lương Net = thu nhập - ngày nghỉ
            Net = EmployeeContract.Salary - Deduction;
            //tính lương quy đổi
            Exchange_Salary = (double)(Net - 11000000 - EmployeeContract.Number_Of_Dependents * 4400000);

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
            TNTT = Net + TTNCN - Deduction;
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
            Company_Paid = SalaryFinal + BHXH_Cmp + BHYT_Cmp + BHTN_Cmp;
        }

        //tạo payslip
        var payslip = new PaySlip
        {
            EmployeeContractId = EmployeeContract.Id,
            Standard_Work_Hours = Standard_Work_Hours,
            Actual_Work_Hours = (int)(Standard_Work_Hours - Leave_Hours),
            Ot_Hours = (int)Ot_Hours,
            Leave_Hours = (int)Leave_Hours,
            Salary = Math.Ceiling(EmployeeContract.Salary.Value),
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
            Paid_date = DateTime.Now,
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
        return payslip.Id;
    }
}