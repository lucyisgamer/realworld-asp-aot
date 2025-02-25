using Microsoft.AspNetCore.Http.HttpResults;

public static partial class Users {
    public static async Task<Results<Ok<UserEnvelope>, ValidationProblem>> Login(IDbContext context, string email, string password) {
        return await context.LoginUser(email, password);
    }
}