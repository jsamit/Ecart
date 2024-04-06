using Catalog.API.Entities;
using Core.Common.CQRS;

namespace Catalog.API.Product.GetProductByCategory;

public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResponse>;
public record GetProductByCategoryResponse(IEnumerable<ProductEO> product);

internal class GetProductByCategoryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
{
    public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ProductEO> products = await session.Query<ProductEO>().Where(p => p.Category.Contains(request.category)).ToListAsync(cancellationToken);
        return new GetProductByCategoryResponse(products);
    }
}
