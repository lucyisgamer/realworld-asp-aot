using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args); // trim much of the fat we don't need for a simple api like this

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // this is required for NativeAOT compatibility, as JSON (de)serialization is normally done via reflection, which won't work in a NativeAOT deployment
    // instead, we need to use the JSON source generator to give compile-time support without reflection
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddAuthentication().AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Realworld",
        ValidAudience = "Realworld_api",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"))
    };
});

WebApplication app = builder.Build();

// The user claim contains the username of the current user, allowing us to quickly retrieve it from the token
AuthorizationPolicy authPolicy = new AuthorizationPolicyBuilder().RequireClaim("Username").RequireAuthenticatedUser().Build();
NpgsqlContext IDbContext = new NpgsqlContext("", new PasswordHasher());

RouteBuilder.BuildRoutes(app, authPolicy, IDbContext);

app.Run();
