using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GodelTech.IdentityServer.Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int? Age { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }

        public ICollection<UserSetting> Settings { get; set; }

        public bool Equals(User other)
        {
            return other?.Id == Id;
        }

        public override bool Equals(object user)
        {
            return !ReferenceEquals(null, user)
                   && (ReferenceEquals(this, user)
                       || user.GetType() == GetType() && Equals((User)user));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
