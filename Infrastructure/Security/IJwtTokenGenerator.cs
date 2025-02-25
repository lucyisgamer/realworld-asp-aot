public interface IJwtTokenGenerator {
    public abstract string GenerateJwtToken(string username);
}