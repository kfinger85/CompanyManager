using Microsoft.AspNetCore.Identity;

namespace CompanyManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public  int AccessFailedCount { get; set; }
        public ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public override string Id { get; set; }
        public string Email { get; set; }
        public ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public string SecurityStamp { get; set; }
    }
}
