using Core.Common.CQRS;
using Catalog.API.Entities;

namespace Catalog.API.Product.CreateProduct
{
    internal record CreateProductRequest(string Name,string Description,string ImageFile,List<string> Category) : ICommand<CreateProductResponse>;
    internal record CreateProductResponse(Guid Id);

    public class CreateProductHandler(IDocumentSession session,ILogger<CreateProductHandler> logger)
        : ICommandHandler<CreateProductRequest, CreateProductResponse>
    {
        async Task<CreateProductResponse> IRequestHandler<CreateProductRequest, CreateProductResponse>.Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateProductHandler.Handle:Start");
            ProductEO product = request.Adapt<ProductEO>();
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            logger.LogInformation("CreateProductHandler.Handle:End");
            return new CreateProductResponse(product.Id);
        }
    }
}
