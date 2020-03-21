using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreZhaoMvc.Auth
{
    public class EmailRequireMent:IAuthorizationRequirement
    {
        public string RequiredEmail { get; set; }
        public EmailRequireMent(string requiredEmail)
        {
            RequiredEmail = requiredEmail;
        }
    }
}
