using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

public static class RouteBuilder {
    public static void BuildRoutes(WebApplication app, AuthorizationPolicy authPolicy, IDbContext IDbContext) {
        RouteGroupBuilder authRequired = app.MapGroup("/api").RequireAuthorization(authPolicy);
        RouteGroupBuilder noAuthRequired = app.MapGroup("/api");
        
        noAuthRequired.MapPost("/users/login", async (string email, string password) => await Users.Login(IDbContext, email, password)); // dummied out for now
        noAuthRequired.MapGet("/users", async(ClaimsPrincipal claims) => await Users.GetCurrentUser(IDbContext, claims));
    }
}