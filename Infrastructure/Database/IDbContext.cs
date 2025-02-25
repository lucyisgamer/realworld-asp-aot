public interface IDbContext {
    public abstract Task<UserEnvelope> CreateUser(string username, string email, string password);
    public abstract Task<UserEnvelope> LoginUser(string email, string password);
    public abstract Task<UserEnvelope> GetUser(string username);
}