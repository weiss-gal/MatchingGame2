using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingGame2.models.user.dto
{
    public class UserInfo
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
        
        // Must start with alphanumetic char followed by any printable chars
        [Required]
        [RegularExpression("^[a-zA-Z0-9][ -~]*")] 
        public string DisplayName { get; set; }
    }
}
