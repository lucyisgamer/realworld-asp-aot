public struct User(string email, string token, string username, string bio = "", string image = "") {
    public string email = email;
    public string token = token;
    public string username = username;
    public string bio = bio;
    public string image = image;
}

public struct UserEnvelope(User user)
{
    public User user = user;
}