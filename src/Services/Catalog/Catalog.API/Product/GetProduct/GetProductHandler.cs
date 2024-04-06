using Catalog.API.Entities;
using Core.Common.CQRS;

namespace Catalog.API.Product.GetProduct;

public record GetProductQuery : IQuery<GetProductResponse>;

public record GetProductResponse(IEnumerable<ProductEO> products);

internal class GetProductHandler(IDocumentSession session, ILogger<GetProductHandler> logger) :
    IQueryHandler<GetProductQuery, GetProductResponse>
{
    public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogDebug("GetProductHandler.Handle:Start");
        IEnumerable<ProductEO> products = await session.Query<ProductEO>().ToListAsync(cancellationToken);
        return new GetProductResponse(products);
    }
}
