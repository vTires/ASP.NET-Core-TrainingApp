namespace ASP.NETCore_TrainingApp.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class CustomPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            // Return the default policy.
            return Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        }

        //public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        //{
        //    // Return the fallback policy.
        //    return Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        //}

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // You can implement your own logic to retrieve policies based on policyName if needed.
            // This method is not directly related to the GetFallbackPolicyAsync method.
            // Return null or a policy based on your requirements.
            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}

