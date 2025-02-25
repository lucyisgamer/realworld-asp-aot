using System.Security.Cryptography;
using System.Text;

public class PasswordHasher: IPasswordHasher {
    // In a proper production implementation, all of this work would be done with specialized hardware, or at least without a hardcoded HMAC key.
    // However, this is a demo app, and that level of complexity simply isn't needed here.
    private readonly HMACSHA3_512 hasher = new(Encoding.ASCII.GetBytes("banjoland"));

    public async Task<byte[]> Hash(byte[] password, byte[] salt) {
        // Combine the password and the salt
        // One might think that using LINQ's Combine function would be simpler, and you'd be right
        // However, LINQ statements have to be interpreted at runtime in an AOT setting, which is much slower than this
        byte[] buffer = new byte[password.Length + salt.Length];
        Buffer.BlockCopy(password, 0, buffer, 0, password.Length);
        Buffer.BlockCopy(salt, 0, buffer, password.Length, salt.Length);

        return await hasher.ComputeHashAsync(new MemoryStream(buffer));
    }
}