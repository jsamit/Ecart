namespace Catalog.API.Product.GetProduct;

public class GetProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async ([AsParameters] GetProductQuery query, ISender sender) =>
        {

            return Results.Ok(await sender.Send(query));
        })
        .Produces<GetProductResponse>(StatusCodes.Status201Created)
        .WithDescription("Get Products")
        .WithName("GetProducts")
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
