namespace SourceFuseSampleAPI.Middleware
{
    public class ApiKeyAuthentication
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";//Key parameter

        /// <summary>
        /// HTTP request pipeline
        /// </summary>
        /// <param name="next"></param>
        public ApiKeyAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        #region API key athentication
        /// <summary>
        /// Http request pipeline method executes in the middleware before the actual request starts processing its activity
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing. ");
                return;
            }
            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEYNAME);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }
            await _next(context);
        }

        #endregion
    }

}
