using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace TryingOutAuth0.Auth
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        const string SCOPE_CLAIM_TYPE = "scope";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!RequestHasCorrectIssuer(context, requirement.Issuer))
                return Task.CompletedTask;

            var scopes = GetScopes(context, requirement.Issuer);

            // Succeed if the scope array contains the required scope
            if (ScopesContainRequiredScope(requirement.Scope, scopes))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private static bool ScopesContainRequiredScope(string requiredScope, string[] scopes) => 
            scopes.Any(s => s == requiredScope);

        private static string[] GetScopes(AuthorizationHandlerContext context, string requiredIssuer) =>
            // Split the scopes string into an array
            context.User.FindFirst(c => c.Type == SCOPE_CLAIM_TYPE && c.Issuer == requiredIssuer).Value.Split(' ');

        private static bool RequestHasCorrectIssuer(AuthorizationHandlerContext context, string requiredIssuer) => 
            context.User.HasClaim(c => c.Type == SCOPE_CLAIM_TYPE && c.Issuer == requiredIssuer);
    }
}
