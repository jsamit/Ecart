using MediatR;

namespace Core.Common.CQRS;

public interface IQuery<TResponse> : IRequest<TResponse>
    where TResponse : notnull;
