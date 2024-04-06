namespace Catalog.API.Product.GetProduct;

public class GetProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (ISender sender) =>
        {
            return Results.Ok(await sender.Send(new GetProductQuery()));
        })
        .Produces<GetProductResponse>(StatusCodes.Status201Created)
        .WithDescription("Get Products")
        .WithName("GetProducts")
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
