
using Catalog.API.Extension;

namespace Catalog.API.Product.UpdateProduct;

public record UpdateProductRequestDTO(Guid Id, string Name, string Description, string ImageFile, List<string> Category, string Summary, decimal Price);

public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("products", async (UpdateProductRequestDTO requestDTO, ISender sender) =>
        {
            return Results.Ok(await sender.Send(requestDTO.Adapt<UpdateProductCommand>()));
        })
        .AddDefaultInfo<UpdateProductResponse>("UpdateProduct", "Update Product");
    }
}
