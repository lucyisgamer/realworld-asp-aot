public static class RouteBuilder {
    public static void CreateRoutes(WebApplication app) {
        RouteGroupBuilder api = app.MapGroup("/api");
        
        api.MapPost("/users/login", () => false); // dummied out for now
    }
}