using Catalog.API.Extension;

namespace Catalog.API.Product.GetProductByCategory;

public class GetProductByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/category/{name}", async (string name, ISender sender) =>
        {
            return Results.Ok(await sender.Send(new GetProductByCategoryQuery(name)));
        })
        .AddDefaultInfo<GetProductByCategoryResponse>("GetProductByCategory", "Get Product By Category");
    }
}
