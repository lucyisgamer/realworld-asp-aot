public struct Comment {
    public ulong id;
    public DateTime createdAt;
    public DateTime updatedAt;
    public string body;
    public Profile author;
}

public struct CommentEnvelope {
    public Comment comment;
}

public struct MultiCommentEnvelope {
    public List<Comment> comments;
}