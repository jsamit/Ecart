namespace Catalog.API.Extension
{
    public static class RouteHandlerBuilderExtension
    {
        public static RouteHandlerBuilder AddDefaultInfo<T>(this RouteHandlerBuilder routeHandler,string name,string description)
        {
            routeHandler
                .Produces<T>(StatusCodes.Status201Created)
                .WithDescription(description)
                .WithName(name)
                .ProducesProblem(StatusCodes.Status400BadRequest);
            return routeHandler;
        }
    }
}
