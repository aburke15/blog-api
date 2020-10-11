
using System.Collections;
using System.Collections.Generic;
using Blog.Api.Controllers.Dtos;
using Blog.Data.Repositories.Interfaces;
using MediatR;

namespace Blog.Api.Infrastructure.Queries
{
    public class GetAllPostSummariesQuery : IRequest<IEnumerable<PostSummaryResponse>>
    { }
}