namespace UserApi.Middlewares;

public class JwtAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        return Task.CompletedTask;
        // var accessToken = context.Request.Headers["Authorization"];
        //
        // Console.WriteLine(accessToken);
        // var tokenHandler = new JwtSecurityTokenHandler();
        //
        // var parameters = new TokenValidationParameters
        // {
        //     ValidAudience = "927017703620-v7c5aqdr40t7ud60v5ohhsdt3949qr45.apps.googleusercontent.com",
        //     IssuerSigningKey = 213,
        //     ValidIssuer = "yourValidIssuer",
        //     ValidateIssuer = true,
        //     ValidateAudience = true,
        //     ClockSkew = TimeSpan.Zero
        // };
        //
        // JwtSecurityTokenHandler
    }
}