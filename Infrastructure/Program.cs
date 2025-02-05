WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args); // trim much of the fat we don't need for a simple api like this

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // this is required for NativeAOT compatibility, as JSON (de)serialization is normally done via reflection, which won't work in a NativeAOT deployment
    // instead, we need to implement source generators to give compile-time support without reflection
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddAuthentication().AddJwtBearer();

WebApplication app = builder.Build();



app.Run();

