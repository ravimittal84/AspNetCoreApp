using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AspNetCoreApp.Security
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DOB { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        // Navigation property for the claims this user possesses.
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();
    }
}
