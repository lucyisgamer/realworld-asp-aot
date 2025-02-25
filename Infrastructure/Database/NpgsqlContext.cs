using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Npgsql;

public class NpgsqlContext {
    private readonly NpgsqlDataSource dataSource;
    private readonly IPasswordHasher passwordHasher;
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public NpgsqlContext(string connectionString, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator) {
        // use a slim builder here to take maximum advantage of trimming.
        // this data source will be used for every query this app makes.
        dataSource = new NpgsqlSlimDataSourceBuilder(connectionString).Build();
        this.passwordHasher = passwordHasher;
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<UserEnvelope> CreateUser(string username, string email, string password) {
        // TODO: ensure duplicate users are disallowed

        byte[] salt = RandomNumberGenerator.GetBytes(64); // 512-bit length, should be plenty for us.
        byte[] hash = await passwordHasher.Hash(Encoding.UTF8.GetBytes(password), salt);

        await using NpgsqlCommand cmd = dataSource.CreateCommand("INSERT INTO users (username, email, passwordHash, salt) VALUES ($1, $2, $3, $4)");
        cmd.Parameters.AddWithValue(username);
        cmd.Parameters.AddWithValue(email);
        cmd.Parameters.AddWithValue(hash);
        cmd.Parameters.AddWithValue(salt);
        await cmd.ExecuteNonQueryAsync();
        return await LoginUser(email, password);
    }

    public async Task<UserEnvelope> LoginUser(string email, string password) {
        await using NpgsqlCommand cmd = dataSource.CreateCommand("SELECT * FROM users WHERE email=$1 LIMIT 1");
        cmd.Parameters.AddWithValue(email);
        await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        byte[] hash = (byte[])reader["passwordHash"];
        byte[] salt = (byte[])reader["salt"];

        if (CryptographicOperations.FixedTimeEquals(hash, await passwordHasher.Hash(Encoding.UTF8.GetBytes(password), salt))) {
            string username = (string)reader["username"];
            string bio = (string)reader["bio"];
            string image = (string)reader["image"];
            string token = jwtTokenGenerator.GenerateJwtToken(username);
            return new UserEnvelope(new User(email, token, username, bio, image));
        } else {
            throw new ValidationException("Incorrect Credentials    ");
        }
    }

    public async Task<UserEnvelope> GetUser(string username) {
        return new UserEnvelope();
    }
}