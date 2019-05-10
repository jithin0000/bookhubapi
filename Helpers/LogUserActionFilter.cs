using System;
using System.Security.Claims;
using System.Threading.Tasks;
using angu.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace angu.Helpers
{
    public class LogUserActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var UserId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContext.HttpContext.RequestServices.GetService<IDatingRespository>();

            var user = await repo.GetUserById(UserId);
            user.LastActive = DateTime.Now;
            await repo.SaveAll();
        }
    }
}