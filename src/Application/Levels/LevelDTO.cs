using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Levels;
public class LevelDTO: IMapFrom<Level>
{
    public Guid RoleId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

}
