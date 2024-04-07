using Catalog.API.Entities;
using Core.Common.CQRS;
using Marten.Pagination;

namespace Catalog.API.Product.GetProduct;

public record GetProductQuery(int? PageNumber,int? PageSize) : IQuery<GetProductResponse>;

public record GetProductResponse(IEnumerable<ProductEO> products);

internal class GetProductHandler(IDocumentSession session, ILogger<GetProductHandler> logger) :
    IQueryHandler<GetProductQuery, GetProductResponse>
{
    public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogDebug("GetProductHandler.Handle:Start");
        IEnumerable<ProductEO> products = await session.Query<ProductEO>().ToPagedListAsync(request.PageNumber ?? 1,request.PageSize ?? 10,cancellationToken);
        return new GetProductResponse(products);
    }
}
