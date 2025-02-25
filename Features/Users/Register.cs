public static partial class Users {
    public static async Task<UserEnvelope> Register(IDbContext context, string username, string email, string password) {
        return await context.CreateUser(username, email, password);
    }
}