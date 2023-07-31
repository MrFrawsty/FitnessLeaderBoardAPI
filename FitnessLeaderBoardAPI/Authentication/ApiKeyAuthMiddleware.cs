namespace FitnessLeaderBoardAPI.Authentication
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var ExtractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key missing");
                return;
            }

            var ApiKey = _config.GetValue<string>(AuthConstants.ApiKeySectionName);

            if (ApiKey != null && !ApiKey.Equals(ExtractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Api Key");
            }

            await _next(context);
        }
    }
}
