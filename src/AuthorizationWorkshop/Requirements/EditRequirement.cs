using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationWorkshop
{
    public class EditRequirement : IAuthorizationRequirement
    {
    }

    public class DocumentEditHandler : AuthorizationHandler<EditRequirement, Document>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            EditRequirement requirement,
            Document resource)
        {
            if (resource.Author == context.User.FindFirst(ClaimTypes.Name).Value)
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }

}