using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

public static partial class Users {
    public static async Task<Results<Ok<UserEnvelope>, UnauthorizedHttpResult>> GetCurrentUser(IDbContext context, ClaimsPrincipal claims) {
        string? username = claims.FindFirstValue("Username");
        if (username is null) {
            // should never be reached, request must have a username claim to be routed here.
            return TypedResults.Unauthorized();
        }
        return TypedResults.Ok<UserEnvelope>(await context.GetUser(username));
    }
}