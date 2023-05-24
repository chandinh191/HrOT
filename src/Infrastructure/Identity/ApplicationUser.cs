using hrOT.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace hrOT.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public DateTime BirthDay { get; set; }
    public virtual Employee Employee { get; set; }
}
