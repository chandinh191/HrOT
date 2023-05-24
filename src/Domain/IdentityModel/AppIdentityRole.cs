using Microsoft.AspNetCore.Identity;

namespace LogOT.Domain.IdentityModel
{
    public class AppIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
