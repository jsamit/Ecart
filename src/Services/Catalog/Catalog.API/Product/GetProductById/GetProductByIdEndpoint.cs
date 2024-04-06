
using Catalog.API.Extension;

namespace Catalog.API.Product.GetProductById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("products/{id:guid}", async (Guid id, ISender sender) =>
            {
                return Results.Ok(await sender.Send(new GetProductByIdQuery(id)));
            })
            .AddDefaultInfo<GetProductByIdResponse>("Get Product by Id", "GetProductById");
        }
    }
}
