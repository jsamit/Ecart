
using Catalog.API.Extension;

namespace Catalog.API.Product.DeleteProduct
{
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{id:guid}", async (Guid id, ISender sender) =>
            {
                return Results.Ok(await sender.Send(new DeleteProductCommand(id)));
            })
            .AddDefaultInfo<DeleteProductResponse>("DeleteProduct", "Delete Product");
        }
    }
}
