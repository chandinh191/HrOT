using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.SalaryCalculators.Commands;
public record CreateSalaryCalculatorCommand : IRequest<SalaryCalculatorDto>
{
    public double Income { get; set; }
    public string InsuranceType { get; set; }
    public double CustomSalary { get; set; }
    public double Number_Of_Dependents { get; set; }
    public string SalaryType { get; set; }

}

public class CreateSalaryCalculatorCommandHandler : IRequestHandler<CreateSalaryCalculatorCommand, SalaryCalculatorDto>
{
    private readonly IApplicationDbContext _context;

    public CreateSalaryCalculatorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SalaryCalculatorDto> Handle(CreateSalaryCalculatorCommand request, CancellationToken cancellationToken)
    {
        //khai báo
        double? Gross = 0;
        double? Net = 0;
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
        double? CmpSalaryFinal = 0;

        var List_TaxInCome = await _context.TaxInComes
            .OrderBy(t => t.Muc_chiu_thue)
            .ToListAsync(cancellationToken);
        var List_Exchange = await _context.Exchanges
            .OrderBy(e => e.Muc_Quy_Doi)
            .ToListAsync(cancellationToken);

        int n = List_TaxInCome.Count();
        int m = List_Exchange.Count();

        double?[] DetailTaxInCome = new double?[n];
        for (int i = 1; i < n; i++)
        {
            DetailTaxInCome[i] = 0;
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

        //nếu là lương NET
        if (request.SalaryType == "Net")
        {
            Gross = request.Income;
            //tính các loại bảo hiểm nhân viên phải trả 
            //tính trên lương
            if (request.InsuranceType == "Official")
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
                BHXH_Emp = request.CustomSalary * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = request.CustomSalary * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = request.CustomSalary * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }

            //tính thu nhập trước thuế
            TNTT = Gross - BHXH_Emp - BHYT_Emp - BHTN_Emp;
            //tính thu nhập chịu thuế
            TNCT = (double)(TNTT - 11000000 - request.Number_Of_Dependents * 4400000);
            // nếu thu nhập chịu thuế > 0 thì tính thuế thu nhập cá nhân
            if (TNCT > 0)
            {
                // Nếu TNCT nhỏ hơn mức chịu thuế đầu tiên
                if (TNCT <= List_TaxInCome[0].Muc_chiu_thue)
                {
                    // Tính thuế cho mức thu nhập chịu thuế là TNCT
                    TTNCN = TNCT * List_TaxInCome[0].Thue_suat;
                    // Thêm giá trị TTNCN vào danh sách TTNCN_Detail
                    DetailTaxInCome[0] = TTNCN;
                }
                else
                {
                    for (int i = 1; i < n; i++)
                    {
                        if (TNCT <= List_TaxInCome[i].Muc_chiu_thue)
                        {
                            TTNCN = (TNCT - List_TaxInCome[i - 1].Muc_chiu_thue) * List_TaxInCome[i].Thue_suat;
                            DetailTaxInCome[i] = DetailTaxInCome[i] = Math.Ceiling(TTNCN.Value);
                            for (int j = 0; j < i; j++)
                            {
                                TTNCN += DetailTaxInCome_Max[j];
                                DetailTaxInCome[j] = DetailTaxInCome_Max[j];
                            }
                            break;
                        }

                    }
                }
            }

            //tính lương cuối
            Net = TNTT - TTNCN;

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
            //tính lương phía công ty phải chi trả
            CmpSalaryFinal = Gross + BHXH_Cmp + BHYT_Cmp + BHTN_Cmp;
        }

        //nếu là lương GROSS
        else
        {
            //tính lương Net = thu nhập - ngày nghỉ
            Net = request.Income;
            //tính lương quy đổi
            Exchange_Salary = (double)(Net - 11000000 - request.Number_Of_Dependents * 4400000);

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
                    DetailTaxInCome[0] = TTNCN;
                }
                else
                {
                    for (int i = 1; i < n; i++)
                    {
                        if (TNCT <= List_TaxInCome[i].Muc_chiu_thue)
                        {
                            TTNCN = (TNCT - List_TaxInCome[i - 1].Muc_chiu_thue) * List_TaxInCome[i].Thue_suat;
                            DetailTaxInCome[i] = Math.Ceiling(TTNCN.Value);
                            for (int j = 0; j < i; j++)
                            {
                                TTNCN += DetailTaxInCome_Max[j];
                                DetailTaxInCome[j] = DetailTaxInCome_Max[j];
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
            if (request.InsuranceType == "Official")
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
                BHXH_Emp = request.CustomSalary * 0.08;
                if (BHXH_Emp > 2384000)
                {
                    BHXH_Emp = 2384000;
                }
                BHYT_Emp = request.CustomSalary * 0.015;
                if (BHYT_Emp > 447000)
                {
                    BHYT_Emp = 447000;
                }
                BHTN_Emp = request.CustomSalary * 0.01;
                if (BHTN_Emp > 884000)
                {
                    BHTN_Emp = 884000;
                }
            }
            //tính lương cuối
            Gross = TNTT + BHXH_Emp + BHYT_Emp + BHTN_Emp;

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
            //tính lương phía công ty phải chi trả
            CmpSalaryFinal = Gross + BHXH_Cmp + BHYT_Cmp + BHTN_Cmp;
        }
        if (TNCT < 0)
        {
            TNCT = 0;
        }
        var Result = new SalaryCalculatorDto
        {
            Gross = Math.Ceiling(Gross.Value),
            BHXH_Emp = Math.Ceiling(BHXH_Emp.Value),
            BHYT_Emp = Math.Ceiling(BHYT_Emp.Value),
            BHTN_Emp = Math.Ceiling(BHTN_Emp.Value),
            TNTT = Math.Ceiling(TNTT.Value),
            Dependent_Deduction = request.Number_Of_Dependents * 4400000,
            TNCT = Math.Ceiling(TNCT.Value),
            TTNCN = Math.Ceiling(TTNCN.Value),
            Net = Math.Ceiling(Net.Value),
            DetailTaxInCome = DetailTaxInCome,
            BHXH_Comp = Math.Ceiling(BHXH_Cmp.Value),
            BHYT_Comp = Math.Ceiling(BHYT_Cmp.Value),
            BHTN_Comp = Math.Ceiling(BHTN_Cmp.Value),
            Total_Cmp_Salary = Math.Ceiling(CmpSalaryFinal.Value)
        };
        return Result;
    }
}
