using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PRN_ProjectBlogs.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetAccountID( this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("AccountID");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetRoleID(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("RoleID");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetAvatar(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Avatar");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimtype)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimtype);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
