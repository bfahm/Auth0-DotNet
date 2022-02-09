using Microsoft.AspNetCore.Authorization;
using System;

namespace TryingOutAuth0.Auth
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }
        
        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException();
            Issuer = issuer ?? throw new ArgumentNullException();
        }
    }
}
