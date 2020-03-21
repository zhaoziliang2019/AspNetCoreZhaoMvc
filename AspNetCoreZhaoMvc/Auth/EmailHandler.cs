using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.Auth
{
    public class EmailHandler : AuthorizationHandler<EmailRequireMent>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailRequireMent requirement)
        {
            var claim = context.User.Claims.FirstOrDefault(x=>x.Type=="Email");
            if (claim!=null)
            {
                if (claim.Value.EndsWith(requirement.RequiredEmail))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
