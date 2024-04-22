using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Filters
{
    public class RateLimitFilterAtribute : ActionFilterAttribute
    {
        private readonly int _limit;
        public RateLimitFilterAtribute(int limit)
        {
            _limit = limit;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ipAdress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            var serivceProvider = context.HttpContext.RequestServices;
            var endPoint = context.HttpContext.GetEndpoint();

            var key = $"{endPoint}{ipAdress}";

            try
            {
                var redis = serivceProvider.GetRequiredService<IRedisCacheConnectionService>().Connection;

                var dataBase = redis.GetDatabase();
                var count = await dataBase.StringIncrementAsync(key);
                if (count > 1)
                {
                    await dataBase.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
                }

                if (count > _limit)
                {
                    context.Result = new StatusCodeResult(429);
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Result = new StatusCodeResult(500);
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
