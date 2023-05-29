using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.PaySlips;
public class DetailTaxInComeDto : IMapFrom<DetailTaxIncome>
{
    public int? Level { get; set; }
    public double? Payment { get; set; }
}