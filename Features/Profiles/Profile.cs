public struct Profile {
    public string username;
    public string bio;
    public string image;
    public bool following;
    public Profile(string Username, bool Following, string Bio = "", string Image = "") {
        username = Username;
        following = Following;
        bio = Bio;
        image = Image;
    }
}

public struct ProfileEnvelope {
    public Profile profile;
}