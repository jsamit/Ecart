using Catalog.API.Entities;
using Catalog.API.Exceptions;
using Core.Common.CQRS;

namespace Catalog.API.Product.UpdateProduct;

public record UpdateProductCommand(Guid Id,string Name, string Description, string ImageFile, List<string> Category) : ICommand<UpdateProductResponse>;
public record UpdateProductResponse(bool isSuccess);

internal class UpdateProductHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ProductEO product = (await session.LoadAsync<ProductEO>(request.Id))!;
        if (product == null)
            throw new ProductnotFoundException();

        product = request.Adapt<ProductEO>();
        session.Update(product);
        await session.SaveChangesAsync();

        return new UpdateProductResponse(true);
    }
}
