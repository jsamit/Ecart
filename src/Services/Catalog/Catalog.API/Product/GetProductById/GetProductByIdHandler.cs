using Catalog.API.Entities;
using Catalog.API.Exceptions;
using Core.Common.CQRS;

namespace Catalog.API.Product.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResponse>;
    public record GetProductByIdResponse(ProductEO product);

    internal class GetProductByIdHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
    {
        public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            ProductEO product = (await session.LoadAsync<ProductEO>(request.id,cancellationToken))!;
            if(product != null)
            {
                return new GetProductByIdResponse(product);
            }
            throw new ProductnotFoundException();
        }
    }
}
