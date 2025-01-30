using System.Text.Json.Serialization;

[JsonSerializable(typeof(User))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}

