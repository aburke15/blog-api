using Microsoft.AspNetCore.Authorization;

namespace Blog.Api
{
    public static class Roles
    {
        public static string Admin => "Admin";
        public static string User => "User";
    }

    public class Policies
    {
        public static AuthorizationPolicy AdminPolicy
            => new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(Roles.Admin)
                .Build();

        public static AuthorizationPolicy UserPolicy
            => new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(Roles.User)
                .Build();
    }
}
