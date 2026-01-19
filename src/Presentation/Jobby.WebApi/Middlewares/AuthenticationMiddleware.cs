namespace Jobby.WebApi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;


        private const string TOKEN = "X-Access-Token";
        private const string REFRESH = "X-Refresh-Token";
        private const string AUTH = "Authorization";
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            bool header = context.Request.Headers.Any(x => x.Key.Equals(AUTH));
            string? token = context.Request.Cookies[TOKEN];
            string? refreshToken = context.Request.Cookies[REFRESH];
            if (!header)
            {
                context.Request.Headers.Append("Authorization", token);
                context.Request.Headers.Append("RefreshToken", refreshToken);
            }
            await _next(context);
        }
    }
}
