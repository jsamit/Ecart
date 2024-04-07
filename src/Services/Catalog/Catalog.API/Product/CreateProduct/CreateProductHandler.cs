using Core.Common.CQRS;
using Catalog.API.Entities;
using FluentValidation;

namespace Catalog.API.Product.CreateProduct
{
    public record CreateProductCommand(string Name,string Description,string ImageFile,List<string> Category) : ICommand<CreateProductResponse>;
    internal record CreateProductResponse(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required").Length(1,150).WithMessage("Maximum Length allowed for Name is 150");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");
        }
    }

    public class CreateProductHandler(IDocumentSession session,ILogger<CreateProductHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        async Task<CreateProductResponse> IRequestHandler<CreateProductCommand, CreateProductResponse>.Handle(CreateProductCommand request, CancellationToken cancellationToken)
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
