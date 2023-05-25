using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace hrOT.Domain.IdentityModel;
public class ApplicationUser : IdentityUser
{
    
    public string Fullname { get; set; }
    public string Address { get; set; } 
    public DateTime BirthDay { get; set; }
    public string Image { get; set; }
    public virtual Employee Employee { get; set; }

}
