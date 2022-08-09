using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Fikra.Filters
{
    public class ClaimAuthorizationFilter
    {
        public class ClaimRequirementFilter : IAuthorizationFilter
        {
            readonly Claim _claim;

            public ClaimRequirementFilter(Claim claim)
            {
                _claim = claim;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var allClaims = context.HttpContext.User.Claims;
                var hasClaim = allClaims.Any(c => c.Type == _claim.Type);

                if (hasClaim == false) context.Result = new UnauthorizedResult();
            }
        }

        public class RequireClaimAttribute : TypeFilterAttribute
        {
            public RequireClaimAttribute(string claimType) : base(typeof(ClaimRequirementFilter))
            {
                Arguments = new object[] { new Claim(claimType, "") };
            }
        }
    }
}
