using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingGame2.database
{
    // No specific use for that at the moment, but creating a placeholder to add some custom user properties
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
