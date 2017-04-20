using System;
using System.Security.Claims;
using System.Security.Principal;

namespace AspBookLibrary.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserAvatar(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity) identity).FindFirst("UserAvatar");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static RoleTypes GetUserRole(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserRole");
            // Test for null to avoid issues during local testing
            return (claim != null) ? (RoleTypes)Enum.Parse(typeof(RoleTypes), claim.Value) : RoleTypes.Member;
        }
    }
}