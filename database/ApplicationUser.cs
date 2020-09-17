using MatchingGame2.models.gameUser;
using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace MatchingGame2.database
{
    // No specific use for that at the moment, but creating a placeholder to add some custom user properties
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
