using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace hrOT.Application.EmployeeContracts.Commands;

public class Employee_CreateContractCommand : IRequest<EmployeeContractDTO>
{
}