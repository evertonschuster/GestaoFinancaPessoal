using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Controllers
{
    public class ClaimsTransformer : IClaimsTransformation
    {

        // Can consume services from DI as needed, including scoped DbContexts
        public ClaimsTransformer(IHttpContextAccessor httpAccessor) { }
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal p)
        {
            p.AddIdentity(new ClaimsIdentity());
            return Task.FromResult(p);
        }

 
    }
}
