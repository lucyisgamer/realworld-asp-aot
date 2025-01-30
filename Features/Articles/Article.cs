public struct Article {
    public string slug;
    public string title;
    public string description;
    public string? body; // when returning multiple articles at once the body is omitted for better performance
    public string[]? tags; // tags are not required when posting an article
    public DateTime createdAt;
    public DateTime updatedAt;
    public bool favorited;
    public ulong favoritesCount; // the api docs aren't clear about required integer sizes for these items, so we'll use ulong just to be safe
    public Profile author;
}