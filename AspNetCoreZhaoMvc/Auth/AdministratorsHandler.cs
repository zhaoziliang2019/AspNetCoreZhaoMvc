using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.Auth
{
    public class AdministratorsHandler : AuthorizationHandler<QualifiedUserRequireMent>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, QualifiedUserRequireMent requirement)
        {
            if (context.User.IsInRole("Adminstrators"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
