using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Filters
{
    public class RateLimitCheckOperation
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasRateLimit = context.MethodInfo.DeclaringType != null && (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<RateLimitFilterAtribute>().Any() ||
                                                                            context.MethodInfo.GetCustomAttributes(true).OfType<RateLimitFilterAtribute>().Any());

            if (!hasRateLimit)
            {
                return;
            }

            operation.Responses.TryAdd("429", new OpenApiResponse { Description = "Too many requests" });
        }
    }
}
