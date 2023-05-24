using Microsoft.AspNetCore.Identity;

namespace hrOT.Domain.IdentityModel
{
    public class AppIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
