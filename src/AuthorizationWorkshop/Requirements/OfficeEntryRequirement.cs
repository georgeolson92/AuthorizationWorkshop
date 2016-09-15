using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace AuthorizationWorkshop.Requirements
{
    public class OfficeEntryRequirement : IAuthorizationRequirement
    {
    }
    // separate classes are on one file due to convenienve/laziness/ignorance
    public class HasBadgeHandler : AuthorizationHandler<OfficeEntryRequirement>
    {
        protected override Task HandleRequirementAsync(
          AuthorizationHandlerContext context,
          OfficeEntryRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "BadgeNumber" &&
                                            c.Issuer == "https://contoso.com"))
            {
                return Task.FromResult(0);
            }

            context.Succeed(requirement);

            return Task.FromResult(0);
        }
    }

    namespace AuthorizationLab
    {
        public class HasTemporaryPassHandler : AuthorizationHandler<OfficeEntryRequirement>
        {
            protected override Task HandleRequirementAsync(
              AuthorizationHandlerContext context,
              OfficeEntryRequirement requirement)
            {
                if (!context.User.HasClaim(c => c.Type == "TemporaryBadgeExpiry" &&
                                                c.Issuer == "https://contoso.com"))
                {
                    return Task.FromResult(0);
                }

                var temporaryBadgeExpiry =
                    Convert.ToDateTime(context.User.FindFirst(
                                           c => c.Type == "TemporaryBadgeExpiry" &&
                                           c.Issuer == "https://contoso.com").Value);

                if (temporaryBadgeExpiry > DateTime.Now)
                {
                    context.Succeed(requirement);
                }

                return Task.FromResult(0);
            }
        }
    }

    public class HasTemporaryPassHandler : AuthorizationHandler<OfficeEntryRequirement>
    {
        protected override Task HandleRequirementAsync(
          AuthorizationHandlerContext context,
          OfficeEntryRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "TemporaryBadgeExpiry" &&
                                            c.Issuer == "https://contoso.com"))
            {
                return Task.FromResult(0);
            }

            var temporaryBadgeExpiry =
                Convert.ToDateTime(context.User.FindFirst(
                                       c => c.Type == "TemporaryBadgeExpiry" &&
                                       c.Issuer == "https://contoso.com").Value);

            if (temporaryBadgeExpiry > DateTime.Now)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
