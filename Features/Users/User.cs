public struct User {
    public string email;
    public string token;
    public string username;
    public string? bio;
    public string? image;
}

public struct UserEnvelope {
    public User user;
}