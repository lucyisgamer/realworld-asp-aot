public interface IPasswordHasher {
       public abstract Task<byte[]> Hash(byte[] password, byte[] salt);
}