using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Core.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal ,string claimType)
        {
            //var userRoles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x=>x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
        }
    }
}
