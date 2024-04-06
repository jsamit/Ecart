namespace Catalog.API.Product.CreateProduct;

public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products",async (CreateProductRequest request, ISender sender) => 
        {
            CreateProductResponse response = await sender.Send(request);
            return Results.Created($"products/{response.Id}", response);
        })
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .WithDescription("Create Product")
        .WithName("CreateProduct")
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
