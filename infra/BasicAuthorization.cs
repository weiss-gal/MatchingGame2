using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MatchingGame2.infra
{
    public enum GamePermission
    {
        None, 
        Participate, 
        Read, 
        Manage
    }
    public class BasicAuthorization
    {
        GamePermission GetGamePermission(ClaimsPrincipal User)
        {
            //TODO: fix, make in a non-stub
            return GamePermission.None;
        }  
    }
}
