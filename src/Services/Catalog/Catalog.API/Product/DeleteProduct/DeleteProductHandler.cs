using Catalog.API.Entities;
using Core.Common.CQRS;

namespace Catalog.API.Product.DeleteProduct;

public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResponse>;

public record DeleteProductResponse(bool isSuccess);

internal class DeleteProductHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        session.Delete<ProductEO>(request.id);
        await session.SaveChangesAsync();
        return new DeleteProductResponse(true);
    }
}
