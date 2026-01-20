using Microsoft.AspNetCore.Authentication;

namespace Jobby.WebApi.Middlewares
{
    public static class ConfigureMiddlewares
    {
        public static void UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
