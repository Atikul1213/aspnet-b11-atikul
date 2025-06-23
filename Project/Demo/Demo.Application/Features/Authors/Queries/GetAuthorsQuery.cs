using Demo.Domain.Entities;
using MediatR;

namespace Demo.Application.Features.Authors.Queries
{
    public class GetAuthorsQuery : IRequest<IList<Author>>
    {
    }
}
