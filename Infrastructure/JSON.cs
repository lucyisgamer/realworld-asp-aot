using System.Text.Json.Serialization;

[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(UserEnvelope))]
[JsonSerializable(typeof(Profile))]
[JsonSerializable(typeof(ProfileEnvelope))]
[JsonSerializable(typeof(Article))]
[JsonSerializable(typeof(ArticleEnvelope))]
[JsonSerializable(typeof(MultiArticleEnvelope))]
[JsonSerializable(typeof(Comment))]
[JsonSerializable(typeof(CommentEnvelope))]
[JsonSerializable(typeof(MultiCommentEnvelope))]
[JsonSerializable(typeof(DateTime))] // not sure if these are needed but they can't hurt
[JsonSerializable(typeof(string))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}

