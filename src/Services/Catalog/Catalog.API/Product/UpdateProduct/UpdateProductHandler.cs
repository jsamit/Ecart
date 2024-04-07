using Catalog.API.Entities;
using Catalog.API.Exceptions;
using Core.Common.CQRS;

namespace Catalog.API.Product.UpdateProduct;

public record UpdateProductCommand(Guid Id,string Name, string Description, string ImageFile, List<string> Category, string Summary, decimal Price) : ICommand<UpdateProductResponse>;
public record UpdateProductResponse(bool isSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required").Length(1, 150).WithMessage("Maximum Length allowed for Name is 150");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is Required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
    }
}

internal class UpdateProductHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ProductEO product = (await session.LoadAsync<ProductEO>(request.Id))!;
        if (product == null)
            throw new ProductNotFoundException(request.Id);

        product = request.Adapt<ProductEO>();
        session.Update(product);
        await session.SaveChangesAsync();

        return new UpdateProductResponse(true);
    }
}
