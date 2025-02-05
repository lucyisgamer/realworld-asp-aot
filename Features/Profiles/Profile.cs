public struct Profile {
    public string username;
    public string? bio;
    public string? image;
    public bool following;
}

public struct ProfileEnvelope {
    public Profile profile;
}